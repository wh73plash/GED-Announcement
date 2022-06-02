using System.Text;
using System.Net;
using System.IO;

static class program {
    static string uri = "https://www.goe.go.kr/home/bbs/bbsList.do?menuId=100000000000063&menuInit=2%2C3%2C3%2C0%2C0&searchTab=&searchCategory=&bbsId=&bbsMasterId=BBSMSTR_000000000112&pageIndex=1&schKey=TITLE&schVal=" + "검정고시+시행계획+공고";
    static string contents = string.Empty;

    static private void process_function(object sender, DownloadStringCompletedEventArgs e) {
        try {
            string html = e.Result;
            contents = html;
            Console.WriteLine("-----start-----\n\n" + contents + "-----finish-----\n\n");
        } catch(Exception ex) {
            Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
        }
        return;
    }

    static private void webclient_get_html(string buffer_uri) {
        try {
            do {
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                wc.DownloadStringAsync(new Uri(buffer_uri));
                wc.DownloadStringCompleted += process_function;
                Thread.Sleep(500);
            } while(contents == String.Empty);
        } catch(Exception ex) {
            Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
        }
        return;
    }

    static private async void httpclient_get_html(string buffer_uri) {
        try {
            HttpClient hc = new HttpClient();
            HttpResponseMessage hrm = await hc.GetAsync(buffer_uri);
            string responseBody = await hrm.Content.ReadAsStringAsync();
            contents = responseBody;
        } catch(Exception ex) {
            Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
        }
        return;
    }

    static bool checking() {
        string[] buff = contents.Split(new string[] { "<td class=\"subj\">" }, StringSplitOptions.None);
        for(int i = 1;i < buff.Length;++i) {
            string buffer = buff[i].Split(new String[] { ";\">" }, StringSplitOptions.None)[1].Split(new String[] { "</a>" }, StringSplitOptions.None)[0];
            if(buffer.Contains("2022") && buffer.Contains("2회")) {
                string path = "/Users/jeinkim/Desktop/시험공고떴습니다.시험공고떴습니다";
                File.WriteAllText(path, string.Empty, Encoding.Default);

                path = "/Users/jeinkim/Desktop/시험공고떴습니다.png";
                File.WriteAllText(path, string.Empty, Encoding.Default);

                path = "/Users/jeinkim/Desktop/시험공고떴습니다.exe";
                File.WriteAllText(path, string.Empty, Encoding.Default);

                on = true;
            }
        }
        return false;
    }

    static bool on = false;

    static void Main(string[] args) {
        try {
            do {
                httpclient_get_html(uri);
                Thread.Sleep(5000);
                on = checking();
            } while(!on);

    } catch(Exception ex) {
            Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
        }

        return;
    }
}