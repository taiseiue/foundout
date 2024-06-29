namespace FindOutDisplay;

public class LLMManager
{
    public System.Diagnostics.Process PyProcess { get; set; }
    public LLMManager()
    {
        //Processオブジェクトを作成
        System.Diagnostics.Process p = new System.Diagnostics.Process();

        //ComSpec(cmd.exe)のパスを取得して、FileNameプロパティに指定
        p.StartInfo.FileName = "python3";
        //出力を読み取れるようにする
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardInput = false;
        p.StartInfo.RedirectStandardError = true;
        p.OutputDataReceived += p_OutputDataReceived;
        p.ErrorDataReceived += p_ErrorDataReceived;

        //ウィンドウを表示しないようにする
        p.StartInfo.CreateNoWindow = true;
        p.StartInfo.WorkingDirectory = "/Users/taiseiue/Documents/source/foundout/backend/";
        //コマンドラインを指定（"/c"は実行後閉じるために必要）
        p.StartInfo.Arguments = @"-u main.py";
        Console.WriteLine(p.StartInfo.Arguments);
        //起動
        p.Start();
        p.BeginOutputReadLine();
        p.BeginErrorReadLine();

        //p.WaitForExit();
        //p.Close();
    }
     void p_OutputDataReceived(object sender,
        System.Diagnostics.DataReceivedEventArgs e)
    {
        string str = e?.Data?.ToString();
        str ??= "";
        if(str.StartsWith("##") && str.EndsWith("##"))
        {
            if(str == "##RESET##")
            {
                CelebrateMode = false;
                Header = "当ててね";
                Word1 = "";
                Word2 = "";
                Word3 = "";
                Word4 = "";
                Word5 = "";
            }
            else if(str == "##ANSWER##")
            {
                CelebrateMode = true;
                Header = "正解！";
                Word1 = "";
                Word2 = "";
                Word3 = "";
                Word4 = "";
                Word5 = "";
            }
            else
            {
                // Pythonから送信されたメッセージ
                string[] words = str.Substring(2, str.Length - 4).Split('|');
                if(words.Length >= 5)
                {
                    Header = "当ててね";
                    Word1 = words[0];
                    Word2 = words[1];
                    Word3 = words[2];
                    Word4 = words[3];
                    Word5 = words[4];
                }
                else if(words.Length >= 4)
                {
                    Header = "当ててね";
                    Word1 = words[0];
                    Word2 = words[1];
                    Word3 = words[2];
                    Word4 = words[3];
                    Word5 = "";
                }
                else if(words.Length >= 3)
                {
                    Header = "当ててね";
                    Word1 = words[0];
                    Word2 = words[1];
                    Word3 = words[2];
                    Word4 = "";
                    Word5 = "";
                }
                else if(words.Length >= 2)
                {
                    Header = "当ててね";
                    Word1 = words[0];
                    Word2 = words[1];
                    Word3 = "";
                    Word4 = "";
                    Word5 = "";
                }
                else if(words.Length >= 1)
                {
                    Header = "当ててね";
                    Word1 = words[0];
                    Word2 = "";
                    Word3 = "";
                    Word4 = "";
                    Word5 = "";
                }
            }
            
        }
    }
    public static bool CelebrateMode {get;set;}
    public static string Header {get;set;}
    public static string Word1 {get;set;}
    public static string Word2 {get;set;}
    public static string Word3 {get;set;}
    public static string Word4 {get;set;}
    public static string Word5 {get;set;}

    static void p_ErrorDataReceived(object sender,
        System.Diagnostics.DataReceivedEventArgs e)
    {
        //エラー出力された文字列を表示する
        Console.WriteLine("ERR>{0}", e.Data);
    }
}