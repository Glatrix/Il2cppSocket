using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Il2cppSocketCli
{
    public class Il2cppSocket
    {
        private HttpClient Client { get; set; }

        public Il2cppSocket()
        {
            Client = new HttpClient();
            Client.Timeout = new TimeSpan(0, 0, 0, 0, 2000);
        }

        public async Task<string?> Call(string endpoint, Dictionary<string, string> paramaters)
        {
            try
            {
                HttpContent content = new StringContent("", System.Text.Encoding.UTF8, "text/plain");
                foreach (var param in paramaters)
                {
                    content.Headers.Add(param.Key, param.Value);
                }
                HttpResponseMessage res = await Client.PostAsync($"http://127.0.0.1:1212/{endpoint}", content);
                res.EnsureSuccessStatusCode();
                return await res.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<string?> Call(string endpoint)
        {
            try
            {
                HttpContent content = new StringContent("", System.Text.Encoding.UTF8, "text/plain");
                HttpResponseMessage res = await Client.PostAsync($"http://127.0.0.1:1212/{endpoint}", content);
                return await res.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> IsActive()
        {
            string? res = await Call("ping");
            return res != null && res == "pong";
        }

        #region DOMAIN

        public async Task<IntPtr> il2cpp_domain_get()
        {
            string? response = await Call("il2cpp_domain_get");
            if (response == null) { return IntPtr.Zero; }

            return IntPtr.Parse(response, System.Globalization.NumberStyles.HexNumber);
        }

        public async Task<IntPtr> il2cpp_domain_assembly_open(IntPtr domain, string name)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_domain", domain.ToString("X") },
                { "p_name", name },
            };

            string? response = await Call("il2cpp_domain_assembly_open", paramaters);
            if (response == null) { return IntPtr.Zero; }

            return IntPtr.Parse(await Call("il2cpp_domain_assembly_open", paramaters) ?? "0", NumberStyles.HexNumber);
        }

        public async Task<List<IntPtr>> il2cpp_domain_get_assemblies(IntPtr domain)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_domain", domain.ToString("X") }
            };

            string? response = await Call("il2cpp_domain_get_assemblies", paramaters);
            if (response == null) { return Array.Empty<IntPtr>().ToList(); }

            IEnumerable<string> pointers = response.Split("\n").Where((l) => !string.IsNullOrWhiteSpace(l));
            return pointers.Select((pointer) => IntPtr.Parse(pointer, NumberStyles.HexNumber)).ToList();
        }

        #endregion

        #region ASSEMBLY

        public async Task<IntPtr> il2cpp_assembly_get_image(IntPtr assembly)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_assembly", assembly.ToString("X") }
            };

            string? response = await Call("il2cpp_assembly_get_image", paramaters);
            if (response == null) { return IntPtr.Zero; }

            return IntPtr.Parse(response, System.Globalization.NumberStyles.HexNumber);
        }

        #endregion

        #region IMAGE

        public async Task<string?> il2cpp_image_get_name(IntPtr image)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_image", image.ToString("X") }
            };

            string? response = await Call("il2cpp_image_get_name", paramaters);
            if (response == null) { return null; }

            return response;
        }

        public async Task<string?> il2cpp_image_get_filename(IntPtr image)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_image", image.ToString("X") }
            };

            string? response = await Call("il2cpp_image_get_filename", paramaters);
            if (response == null) { return null; }

            return response;
        }

        public async Task<int?> il2cpp_image_get_class_count(IntPtr image)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_image", image.ToString("X") }
            };

            string? response = await Call("il2cpp_image_get_class_count", paramaters);
            if (response == null) { return null; }

            int.TryParse(response, out int ret);
            return ret;
        }

        public async Task<IntPtr> il2cpp_image_get_class(IntPtr image, int index)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_image", image.ToString("X") },
                { "p_index", index.ToString() }
            };

            string? response = await Call("il2cpp_image_get_class", paramaters);
            if (response == null) { return IntPtr.Zero; }

            return IntPtr.Parse(response, System.Globalization.NumberStyles.HexNumber);
        }

        public async Task<IntPtr> il2cpp_class_from_name(IntPtr image, string namespaze, string name)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_image", image.ToString("X") },
                { "p_namespace", namespaze },
                { "p_name", name },
            };

            string? response = await Call("il2cpp_class_from_name", paramaters);
            if (response == null) { return IntPtr.Zero; }

            return IntPtr.Parse(response, System.Globalization.NumberStyles.HexNumber);
        }

        #endregion

        #region CLASS

        public async Task<string?> il2cpp_class_get_name(IntPtr klass)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_class", klass.ToString("X") }
            };

            string? response = await Call("il2cpp_class_get_name", paramaters);
            if (response == null) { return null; }

            return response;
        }

        public async Task<IntPtr> il2cpp_class_get_type(IntPtr klass)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_class", klass.ToString("X") }
            };

            string? response = await Call("il2cpp_class_get_type", paramaters);
            if (response == null) { return IntPtr.Zero; }

            return IntPtr.Parse(response, System.Globalization.NumberStyles.HexNumber);
        }

        public async Task<List<IntPtr>> il2cpp_class_get_fields(IntPtr klass)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_class", klass.ToString("X") }
            };

            string? response = await Call("il2cpp_class_get_fields", paramaters);
            if (response == null) { return Array.Empty<IntPtr>().ToList(); }

            IEnumerable<string> pointers = response.Split("\n").Where((l) => !string.IsNullOrWhiteSpace(l));
            return pointers.Select((pointer) => IntPtr.Parse(pointer, NumberStyles.HexNumber)).ToList();
        }

        public async Task<List<IntPtr>> il2cpp_class_get_methods(IntPtr klass)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_class", klass.ToString("X") }
            };

            string? response = await Call("il2cpp_class_get_methods", paramaters);
            if (response == null) { return Array.Empty<IntPtr>().ToList(); }

            IEnumerable<string> pointers = response.Split("\n").Where((l) => !string.IsNullOrWhiteSpace(l));
            return pointers.Select((pointer) => IntPtr.Parse(pointer, NumberStyles.HexNumber)).ToList();
        }

        public async Task<List<IntPtr>> il2cpp_class_get_properties(IntPtr klass)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_class", klass.ToString("X") }
            };

            string? response = await Call("il2cpp_class_get_properties", paramaters);
            if (response == null) { return Array.Empty<IntPtr>().ToList(); }

            IEnumerable<string> pointers = response.Split("\n").Where((l) => !string.IsNullOrWhiteSpace(l));
            return pointers.Select((pointer) => IntPtr.Parse(pointer, NumberStyles.HexNumber)).ToList();
        }

        public async Task<IntPtr> il2cpp_class_get_method_from_name(IntPtr klass, string name, int argsCount)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_class", klass.ToString("X") },
                { "p_name", name },
                { "p_argsCount", argsCount.ToString() },
            };

            string? response = await Call("il2cpp_class_get_method_from_name", paramaters);
            if (response == null) { return IntPtr.Zero; }

            return IntPtr.Parse(response, System.Globalization.NumberStyles.HexNumber);
        }

        public async Task<IntPtr> il2cpp_class_get_field_from_name(IntPtr klass, string name)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_class", klass.ToString("X") },
                { "p_name", name }
            };

            string? response = await Call("il2cpp_class_get_field_from_name", paramaters);
            if (response == null) { return IntPtr.Zero; }

            return IntPtr.Parse(response, System.Globalization.NumberStyles.HexNumber);
        }

        #endregion

        #region TYPE

        public async Task<string?> il2cpp_type_get_name(IntPtr type)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_type", type.ToString("X") }
            };

            string? response = await Call("il2cpp_type_get_name", paramaters);
            if (response == null) { return null; }

            return response;
        }

        #endregion

        #region FIELD

        public async Task<IntPtr> il2cpp_field_static_get_value(IntPtr field)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_field", field.ToString("X") }
            };

            string? response = await Call("il2cpp_field_static_get_value", paramaters);
            if (response == null) { return IntPtr.Zero; }

            return IntPtr.Parse(response, System.Globalization.NumberStyles.HexNumber);
        }

        #endregion

        #region METHOD

        public async Task<IntPtr> socket_create_params(int count)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_count", count.ToString() }
            };

            string? response = await Call("socket_create_params", paramaters);
            if (response == null) { return IntPtr.Zero; }

            return IntPtr.Parse(response, System.Globalization.NumberStyles.HexNumber);
        }

        public async Task socket_set_param(IntPtr paramz, IntPtr value, int index)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_params", paramz.ToString("X") },
                { "p_value",  value.ToString("X") },
                { "p_index",  index.ToString() },
            };

            await Call("socket_set_param", paramaters);
        }

        public async Task<IntPtr> il2cpp_runtime_invoke(IntPtr method, IntPtr _object, IntPtr _params)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_method", method.ToString("X") },
                { "p_params",  _params.ToString("X") },
                { "p_object",  _object.ToString("X") },
            };

            string? response = await Call("il2cpp_runtime_invoke", paramaters);

            if (response == null)
            {
                return IntPtr.Zero;
            }
            string[] items = response.Split("\n");

            if (items[0] == "ERROR")
            {
                // Eventually we can read error?
                throw new Exception("Il2cpp Exception Placeholder");
            }
            else if (items[0] == "RETURN")
            {
                return IntPtr.Parse(items[1], System.Globalization.NumberStyles.HexNumber);
            }

            return IntPtr.Zero;
        }

        #endregion

        #region STRING

        public async Task<IntPtr> il2cpp_string_new(string value)
        {
            Dictionary<string, string> paramaters = new Dictionary<string, string>()
            {
                { "p_value", value }
            };

            string? response = await Call("il2cpp_string_new", paramaters);
            if (response == null) { return IntPtr.Zero; }

            return IntPtr.Parse(response, System.Globalization.NumberStyles.HexNumber);
        }

        #endregion
    }
}
