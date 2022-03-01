using System.Diagnostics;
using System.Text;
using System.Web;
using Microsoft.Win32;

class Program
{
    private static string novr = "--no-vr";
    
    static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            var vrcw = "--url=create?roomId=" + RandomNumbers(10) + "&hidden=true&name=BuildAndRun&url=file:///" + HttpUtility.UrlEncode(args[0]);
            
            Console.WriteLine("Starting Path: " + vrcw);

            #region GetVrchatDIR

            RegistryKey? regKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall");
            var vrcpath = FindByDisplayName(regKey, "VRChat");
            var process = vrcpath + "\\vrchat.exe";
            
            #endregion

            #region CreatedProcessArugments

            var argumentVr = vrcw;
            var argumentNoVr = vrcw + $" {novr}";

            #endregion
            
            Console.WriteLine("would you like to start in VR?: Yes/No");
            
            string userinput = Console.ReadLine();
            bool result = userinput.IndexOf("Yes", StringComparison.OrdinalIgnoreCase) >= 0;
            if (result)
            {
                Process.Start(process, argumentVr);
                Console.Write("Task Complete Starting VR Exiting......");
                Thread.Sleep(5000);
                Environment.Exit(0);
            }
            else
            {
                Process.Start(process, argumentNoVr);
                Console.Write("Task Complete Starting Desktop Exiting......");
                Thread.Sleep(5000);
                Environment.Exit(0);
            }
        }
        else
        {
            Console.Write("this is a drag drop process, drag and drop your VRCW onto me :)\nclosing now");
            Thread.Sleep(2000);
            
            Environment.Exit(0);
        }
    }
    
    private static string FindByDisplayName(RegistryKey? parentKey, string name)
    {
        string[] nameList = parentKey.GetSubKeyNames();
        for (int i = 0; i < nameList.Length; i++)
        {
            RegistryKey regKey =  parentKey.OpenSubKey(nameList[i]);
            try
            {
                if (regKey.GetValue("DisplayName").ToString() == name)
                {
                    return regKey.GetValue("InstallLocation").ToString();
                }
            }
            catch { }
        }
        return "";
    }
    
    private static string RandomNumbers(int Length)
    {
        Random Rand = new Random();
        StringBuilder SB = new StringBuilder();
        for (int i = 0; i < Length; i++)
            SB.Append(Rand.Next(0, 9));

        return SB.ToString();
    }
}