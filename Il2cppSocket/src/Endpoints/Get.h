#pragma once

void RegisterGet_Defaults(Server* svr) {
	// Basic Page for GET Request
	svr->Get("/", [&](const Request& req, Response& res) { res.set_content("<h1>Il2cpp Socket!</h1>", "text/html"); });

	//Ping / Pong to check if socket is running
	svr->Post("/ping", [&](const Request& req, Response& res, const ContentReader& content_reader) { res.body = "pong"; });
}