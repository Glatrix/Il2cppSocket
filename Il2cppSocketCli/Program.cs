using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Il2cppSocketCli
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Il2cppSocket socket = new Il2cppSocket();
            if (await socket.IsActive())
            {
                Console.WriteLine("Socket Is Active!");

                IntPtr domain = await socket.il2cpp_domain_get();
                Console.WriteLine($"Domain: {domain.ToString("X")}");

                IntPtr assembly = await socket.il2cpp_domain_assembly_open(domain, "Assembly-CSharp");
                IntPtr image = await socket.il2cpp_assembly_get_image(assembly);

                IntPtr PlayerControl_c = await socket.il2cpp_class_from_name(image, "", "PlayerControl");
                IntPtr RpcSetName_m = await socket.il2cpp_class_get_method_from_name(PlayerControl_c, "RpcSetName", 1);
                IntPtr LocalPlayer_static_f = await socket.il2cpp_class_get_field_from_name(PlayerControl_c, "LocalPlayer");

                IntPtr LocalPlayer = await socket.il2cpp_field_static_get_value(LocalPlayer_static_f);

                IntPtr newName = await socket.il2cpp_string_new("1\n2\n3");
                IntPtr paramaters = await socket.socket_create_params(1);

                await socket.socket_set_param(paramaters, newName, 0);
                IntPtr intptr = await socket.il2cpp_runtime_invoke(RpcSetName_m, LocalPlayer, paramaters);
                Console.WriteLine($"RET: {intptr.ToString("X")}");
            }
            else
            {
                Console.WriteLine("Socket Inactive");
            }
        }
    }
}