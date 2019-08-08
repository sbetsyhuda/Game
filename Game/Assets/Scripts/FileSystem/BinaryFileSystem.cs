using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Assets.Scripts.FileSystem
{
    public static class BinaryFileSystem
    {
        //not finished
        public static List<string> LoadFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Open);
                BinaryWriter binaryWriter = new BinaryWriter(fileStream);

                #region FileLoad

                #endregion

            }
            else
            {
                //error
            }
            return new List<string>();  
        }

        //not finished
        public static void SaveFile(string filePath, List<string> data)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);

            #region WriteToBinaryFile

            #endregion

            binaryWriter.Close();
            fileStream.Close();

        }
    }
}
