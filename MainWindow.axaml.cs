using System;
using System.Threading;
using Avalonia.Controls;
using Avalonia.Platform;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pookie;

public partial class MainWindow : Window
{

    public async Task<int> FetchCookiesFromWebViewAsync(NativeWebView webView)
    {
        // 1. Try to obtain the cookie manager from the WebView control
        var cookieManager = webView.TryGetCookieManager();

        if (cookieManager != null)
        {
            // 2. Await GetCookiesAsync() to retrieve the read-only list of System.Net.Cookie
            IReadOnlyList<Cookie> cookies = await cookieManager.GetCookiesAsync();

            // 3. Iterate over the returned collection
            foreach (Cookie cookie in cookies)
            {
                Console.WriteLine($"Name: {cookie.Name}");
                Console.WriteLine($"Value: {cookie.Value}");
                Console.WriteLine($"Domain: {cookie.Domain}");
                Console.WriteLine($"Path: {cookie.Path}");
                Console.WriteLine($"Expires: {cookie.Expires}");
                Console.WriteLine($"HttpOnly: {cookie.HttpOnly}");
                Console.WriteLine("-----------------------------------");
            }
        }
        else
        {
            Console.WriteLine("Cookie management is not supported on this platform/engine.");
            return 0;
        }

        return 1;
    }
    public MainWindow()
    {
        var dialog = new NativeWebDialog
        {
            Title = "Avalonia Docs",
            CanUserResize = false,
            Source = new Uri("https://open.spotify.com/home")
        };


        dialog.NavigationCompleted += (s, e) =>
        {
            if (e.IsSuccess)
            {
                Console.WriteLine("Web dialogue was successfully created ");
                Console.WriteLine("Time to look up How to get the cookies creates");
                Thread.Sleep(500);

                var cookies = dialog.TryGetCookieManager();
                if (cookies == null)
                {
                    Console.WriteLine("Not surported");
                }
            }
        };

        // var networkContext = WebKit.WebsiteDataManager.GetDefault();

        InitializeComponent();
        dialog.Show();
    }

    private async void WebView_NavigationCompleted(object? sender, WebViewNavigationCompletedEventArgs args)
    {
        // Execute JavaScript
        // await webView.InvokeScript("alert('Hello World')");
        Console.WriteLine("Navigation Complete");
    }
}