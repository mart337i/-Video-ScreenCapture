using System.IO;

namespace ScreenCapture
{
    public class Analysis
    {
        public void frames()
        {
            string filePath = @"C:\Users\Egeskov\RiderProjects\ScreenCapture\ScreenCapture\bin\Debug\out.avi";
            
            FileStream fs = null;
            BinaryReader br = null;
            
            fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            br = new BinaryReader(fs);

            
            int chunkSize = 1024 ; // assumed as 1kb you can extend as per your requirement

            //Here is your byte array
            byte[] buffer = br.ReadBytes(chunkSize);//reading the bytes 
        }
    }
}