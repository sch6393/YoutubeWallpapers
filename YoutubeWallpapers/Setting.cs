using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace YoutubeWallpapers
{
    public class Setting
    {
        public enum IDType
        {
            Single,
            List,
        }

        public enum VideoQuality
        {
            Auto,
            P720,
            P1080,
            P1440,
        }

        public IDType enumIdType
        {
            get;
            set;
        } = IDType.Single;

        public VideoQuality enumVideoQuality
        {
            get;
            set;
        } = VideoQuality.Auto;

        public string strAddress
        {
            get;
            set;
        } = "";

        public string strNumber
        {
            get;
            set;
        } = "1";

        public int iBrightness
        {
            get;
            set;
        } = 50;

        public int iMonitor
        {
            get;
            set;
        } = 0;

        public int iVolume
        {
            get;
            set;
        } = 0;

        /// <summary>
        /// 파일에 저장
        /// </summary>
        /// <param name="strFilename"></param>
        public void SaveToFile(string strFilename)
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(new FileStream(strFilename, FileMode.Create)))
            {
                binaryWriter.Write((int)enumIdType);
                binaryWriter.Write((int)enumVideoQuality);
                binaryWriter.Write(strAddress);
                binaryWriter.Write(strNumber);
                binaryWriter.Write(iBrightness);
                binaryWriter.Write(iMonitor);
                binaryWriter.Write(iVolume);

                binaryWriter.Close();
            }
        }

        /// <summary>
        /// 파일에서 읽기
        /// </summary>
        /// <param name="strFilename"></param>
        public void LoadFromFile(string strFilename)
        {
            using (BinaryReader binaryReader = new BinaryReader(new FileStream(strFilename, FileMode.Open)))
            {
                try
                {
                    enumIdType = (IDType)binaryReader.ReadInt32();;
                    enumVideoQuality = (VideoQuality)binaryReader.ReadInt32();
                    strAddress = binaryReader.ReadString();
                    strNumber = binaryReader.ReadString();
                    iBrightness = binaryReader.ReadInt32();
                    iMonitor = binaryReader.ReadInt32();
                    iVolume = binaryReader.ReadInt32();
                }
                catch
                {

                }
                finally
                {
                    binaryReader.Close();
                }
            }
        }
    }
}
