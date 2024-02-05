using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.Http;
using System.Net.Http.Headers;
using Raylib_cs;

public static class WebClient {

    public static void Main() {

        string resStr = null;

        Raylib.InitWindow(800, 600, "杰克浏览器");

        // 每次连接, 只能进行一次请求
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("http://localhost:5250");

        while (!Raylib.WindowShouldClose()) {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);
            if (Raylib.IsKeyPressed(KeyboardKey.W)) {
                string str = GetHome(client);
                resStr = str;
            }

            if (!string.IsNullOrEmpty(resStr)) {
                Raylib.DrawText(resStr, 10, 10, 20, Color.Black);
            }
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();

    }

    static void GetDL(HttpClient client) {
        HttpResponseMessage response = client.GetAsync("/dl").Result;
        byte[] data = response.Content.ReadAsByteArrayAsync().Result;
        Console.WriteLine(data.Length);
        File.WriteAllBytes("dl.zip", data);
    }

    static string GetHome(HttpClient client) {
        // Http 客户端请求时, 从服务端回来的数据全都是 字节流-byte[]
        HttpResponseMessage response = client.GetAsync("/").Result;
        byte[] data = response.Content.ReadAsByteArrayAsync().Result;
        Console.WriteLine(data.Length);

        return Encoding.UTF8.GetString(data);
    }

}