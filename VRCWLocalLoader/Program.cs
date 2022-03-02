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
            bool extension = vrcw.IndexOf("VRCW", StringComparison.OrdinalIgnoreCase) >= 0;
            if (!extension)
            {
                Console.WriteLine("wrong file extension provided, please provide a .vrcw");
                Thread.Sleep(5000);
                Environment.Exit(0);
            }
            
            Console.WriteLine("Starting with Path: " + vrcw);

            #region GetVrchatDIR

            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\VRChat");
            var vrcpath = registryKey.GetValue(null,"VRChat");
            var process = vrcpath + "\\VRChat.exe";
            
            #endregion

            #region CreatedProcessArugments

            var argumentVr = vrcw;
            var argumentNoVr = vrcw + $" {novr}";

            #endregion
            
            Console.WriteLine("would you like to start in VR?: Yes/No");
            
            string userinput = Console.ReadLine();
            bool userresult = userinput.IndexOf("Yes", StringComparison.OrdinalIgnoreCase) >= 0;
            if (userresult)
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
            Thread.Sleep(5000);
            
            Environment.Exit(0);
        }
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