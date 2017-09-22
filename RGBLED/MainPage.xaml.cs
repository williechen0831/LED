using System;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using APA102;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;



// 空白頁項目範本已記錄在 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x404

namespace RGBLED
{
   
    public class LEDStatus
    {
        public bool Red { get; set; }
        public bool Green { get; set; }
        public bool Blue { get; set; }
    }
    static class determine
    {
        public static List<int> FindIndex(this int[] ArrayInput, int Value)
        {
            List<int> IndexList = new List<int>();
            object IndexListLock = new object();

            Parallel.For(0, ArrayInput.Length, (Index) => {
                if (ArrayInput[Index] == Value)
                    lock (IndexListLock)
                        IndexList.Add(Index);
            });

            return IndexList;
        }
    }
     /// <summary>
     /// 可以在本身使用或巡覽至框架內的空白頁面。
     /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<LEDStatus> LED = new List<LEDStatus>();
        private LED strip = new LED(60, 10, 11, APA102.LED.LED_RGB);
        

        public MainPage()
        {
            
            this.InitializeComponent();
            Initialize();
            web();
        }
        

        private void Initialize()
        {
                strip.BeginAsync();
                LEDTest();
        }

        private void LEDTest()
        {
            for (int i = 0; i < 60; i++)
                LED.Add(new LEDStatus { Blue = false, Green = false, Red = false });
            for (int i = 0; i < 60; i++)
            {
                LED[i].Red = true;
                LED[i].Blue = false;
                LED[i].Green = false;
                LEDEvent();
            }
            for (int i = 59; i >= 0; i--)
            {
                LED[i].Red = false;
                LED[i].Blue = false;
                LED[i].Green = true;
                LEDEvent();
            }
            for (int i = 0; i < 60; i++)
            {
                LED[i].Red = false;
                LED[i].Blue = true;
                LED[i].Green = false;
                LEDEvent();
            }
            for (int i = 59; i >= 0; i--)
            {
                LED[i].Red = false;
                LED[i].Blue = false;
                LED[i].Green = false;
                LEDEvent();
            }
        }
        private async void web()
        {
   
            float[] dis = new float[6];
            int[] block = new int[6];

            string WebURL = "http://140.125.45.186:8087/api/led";
            string Result = await AccessURLAsync(WebURL);
            textblock3.Text = Result;
            JObject obj = JsonConvert.DeserializeObject<JObject>(Result);
                
            string[] listdis = obj["listDis"].ToString().Replace("[", "").Replace("]", "").Split(',');
            string[] listBlock = obj["listBlock"].ToString().Replace("[", "").Replace("]", "").Split(',');
            textbox.Text = obj["sData"].ToString();
            textbox4.Text = obj["vData"].ToString();
            
            for (int i = 0; i < 6; i++)
                dis[i] = float.Parse(listdis[i]); 
            for (int i = 0; i < 6; i++)
                block[i] = int.Parse(listBlock[i]);

            float anInteger4;
            anInteger4 = Convert.ToSingle(textbox4.Text);
            double yellowarea = anInteger4 * 1.75 + (long)Math.Pow(anInteger4, 2) / 20;
            
            var search1 = determine.FindIndex(block, 1);
            if (search1.Count == 0)
                textbox1.Text = (yellowarea + 5).ToString();
            else
                textbox1.Text = blockdis(dis, search1).ToString();

            var search2 = determine.FindIndex(block, 2);
            
            if (blockdis(dis, search2) == 0 || search2.Count==0)
                textbox2.Text = (yellowarea + 5).ToString();
            else
                textbox2.Text = blockdis(dis, search2).ToString();

            var search3 = determine.FindIndex(block, 3);
            if (search3.Count == 0)
                textbox3.Text = (yellowarea + 5).ToString();
            else
                textbox3.Text = blockdis(dis, search3).ToString();
 
            Task.Delay(1000).Wait();
           web();
        }
        private static float blockdis(float[] num,List<int> search)
        {
            List<float> blockdis = new List<float>();
            foreach (int q in search)
                if(num[q]!=0)
                    blockdis.Add(num[q]);
            blockdis.Sort();
            if (blockdis.Count == 0)
                return 0;
            else
                return blockdis[0];
        }
        private static async Task<string> AccessURLAsync(string URL)
        {

            WebRequest webRequest = WebRequest.Create(@URL);
            webRequest.Method = "GET";

            WebResponse webResponse = await webRequest.GetResponseAsync();
            StreamReader sr = new StreamReader(webResponse.GetResponseStream());

            string Result = sr.ReadToEnd();

            sr.Dispose();
            webResponse.Dispose();

            return Result;
        }
        
        private void LEDEvent()
        {
            for (int i = 0; i < 60; i++)
                strip.SetPixelColor(i, 1, Switch2byte(LED[i].Red), Switch2byte(LED[i].Green), Switch2byte(LED[i].Blue));
            strip.Show();
            //System.Threading.Tasks.Task.Delay(100).Wait();
        }

        private byte Switch2byte(bool Switch)
        {
            if (Switch)
                return 255;
            return 0;
        }
        
            private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
            {
                
            }

            private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
            {
            float anInteger1;
            anInteger1 = Convert.ToSingle(textbox1.Text);
            float anInteger4;
            anInteger4 = Convert.ToSingle(textbox4.Text);
            double redarea = anInteger4 * 3600 / 2000;
            double yellowarea = anInteger4 * 1.75 + (long)Math.Pow(anInteger4, 2) / 20;
            
                if (textbox.Text == "1")
                {

                    for (int i = 0; i < 40; i++)
                    {
                        LED[i].Red = false;
                        LED[i].Green = false;
                        LED[i].Blue = false;
                    }
                    if (anInteger4 == 0)
                    {
                        if (anInteger1 < 2.5)
                        {
                        for (int i = 0; i < 39; i++)
                        {
                            LED[i].Red = true;
                            LED[i].Green = false;
                            LED[i].Blue = false;
                        }
                        }
                        else
                        {
                        for (int i = 0; i < 39; i++)
                        {
                            LED[i].Green = true;
                            LED[i].Red = false;
                            LED[i].Blue = false;
                        }
                        }
                    }
                    else
                    {
                        if (anInteger1 < redarea)
                        {
                        for (int i = 0; i < 39; i++)
                        {
                            LED[i].Red = true;
                            LED[i].Green = false;
                            LED[i].Blue = false;
                        }
                        }
                        if (anInteger1 >= redarea && anInteger1 < yellowarea)
                        {
                        for (int i = 0; i < 39; i++)
                        {
                            LED[i].Red = true;
                            LED[i].Green = true;
                            LED[i].Blue = false;
                        }
                        }
                        if (anInteger1 >= yellowarea)
                        {
                        for (int i = 0; i < 39; i++)
                        {
                            LED[i].Green = true;
                            LED[i].Red = false;
                            LED[i].Blue = false;
                        }
                        }
                    }
                }
                if (textbox.Text == "2")
                {
                    for (int i = 0; i < 15; i++)
                    {
                        LED[i].Red = false;
                        LED[i].Green = false;
                        LED[i].Blue = false;
                    }
                    if (anInteger4 == 0)
                    {
                        if (anInteger1 < 2.5)
                        {
                        for (int i = 0; i < 14; i++)
                        {
                            LED[i].Red = true;
                            LED[i].Green = false;
                            LED[i].Blue = false;
                        }
                        }
                        else
                        {
                        for (int i = 0; i < 14; i++)
                        {
                            LED[i].Green = true;
                            LED[i].Red = false;
                            LED[i].Blue = false;
                        }
                        }
                    }
                    else
                    {
                        if (anInteger1 < redarea)
                        {
                        for (int i = 0; i < 14; i++)
                        {
                            LED[i].Red = true;
                            LED[i].Green = false;
                            LED[i].Blue = false;
                        }
                        }
                        if (anInteger1 >= redarea && anInteger1 < yellowarea)
                        {
                        for (int i = 0; i < 14; i++)
                        {
                            LED[i].Red = true;
                            LED[i].Green = true;
                            LED[i].Blue = false;
                        }
                        }
                        if (anInteger1 >= yellowarea)
                        {
                        for (int i = 0; i < 14; i++)
                        {
                            LED[i].Green = true;
                            LED[i].Red = false;
                            LED[i].Blue = false;
                        }
                        }
                    }
                }        
                LEDEvent();          
        }
            private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
            {
                float anInteger2;
                anInteger2 = Convert.ToSingle(textbox2.Text);
                float anInteger4;
                anInteger4 = Convert.ToSingle(textbox4.Text);
                double redarea = anInteger4 * 3600 / 2000;
                double yellowarea = anInteger4 * 1.75 + (long)Math.Pow(anInteger4, 2) / 20;
            if (textbox.Text == "1")
            {
                for (int i = 40; i < 60; i++)
                {
                        LED[i].Red = false;
                        LED[i].Green = false;
                        LED[i].Blue = false;
                }
                if (anInteger4 == 0)
                {
                    if (anInteger2 < 2.5)
                    {
                        for (int i = 41; i < 60; i++)
                        {
                            LED[i].Red = true;
                            LED[i].Green = false;
                            LED[i].Blue = false;
                        }
                    }
                    else
                    {
                        for (int i = 41; i < 60; i++)
                        {
                            LED[i].Green = true;
                            LED[i].Red = false;
                            LED[i].Blue = false;
                        }
                    }
                }
                else
                {
                    if (anInteger2 < redarea)
                    {
                        for (int i = 41; i < 60; i++)
                        {
                            LED[i].Red = true;
                            LED[i].Green = false;
                            LED[i].Blue = false;
                        }
                    }
                    if (anInteger2 >= redarea && anInteger2 < yellowarea)
                    {
                        for (int i = 41; i < 60; i++)
                        {
                            LED[i].Red = true;
                            LED[i].Green = true;
                            LED[i].Blue = false;
                        }
                    }
                    if (anInteger2 >= yellowarea)
                    {
                        for (int i = 41; i < 60; i++)
                        {
                            LED[i].Green = true;
                            LED[i].Red = false;
                            LED[i].Blue = false;
                        }
                    }
                }
            }
           
            if (textbox.Text == "2")
            {
                for (int i = 15; i < 45; i++)
                {
                        LED[i].Red = false;
                        LED[i].Green = false;
                        LED[i].Blue = false;
                }
                if (anInteger4 == 0)
                {
                    if (anInteger2 < 2.5)
                    {
                        for (int i = 16; i < 45; i++)
                        {
                            LED[i].Red = true;
                            LED[i].Green = false;
                            LED[i].Blue = false;
                        }
                    }
                    else
                    {
                        for (int i = 16; i < 45; i++)
                        {
                            LED[i].Green = true;
                            LED[i].Red = false;
                            LED[i].Blue = false;
                        }
                    }
                }
                else
                {
                    if (anInteger2 < redarea)
                    {
                        for (int i = 16; i < 45; i++)
                        {
                            LED[i].Red = true;
                            LED[i].Green = false;
                            LED[i].Blue = false;
                        }
                    }
                    if (anInteger2 >= redarea && anInteger2 < yellowarea)
                    {
                        for (int i = 16; i < 45; i++)
                        {
                            LED[i].Red = true;
                            LED[i].Green = true;
                            LED[i].Blue = false;
                        }
                    }
                    if (anInteger2 >= yellowarea)
                    {
                        for (int i = 16; i < 45; i++)
                        {
                            LED[i].Green = true;
                            LED[i].Red = false;
                            LED[i].Blue = false;
                        }
                    }
                }
            }
                if (textbox.Text == "3")
                {
                    for (int i = 0; i < 15; i++)
                    {
                        LED[i].Red = false;
                        LED[i].Green = false;
                        LED[i].Blue = false;
                    }
                    if (anInteger4 == 0)
                    {
                        if (anInteger2 < 2.5)
                        {
                            for (int i = 0; i < 14; i++)
                            {
                                LED[i].Red = true;
                                LED[i].Green = false;
                                LED[i].Blue = false;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < 14; i++)
                            {
                                LED[i].Green = true;
                                LED[i].Red = false;
                                LED[i].Blue = false;
                            }
                        }
                    }
                    else
                    {
                        if (anInteger2 < redarea)
                        {
                            for (int i = 0; i < 14; i++)
                            {
                                LED[i].Red = true;
                                LED[i].Green = false;
                                LED[i].Blue = false;
                            }
                        }
                        if (anInteger2 >= redarea && anInteger2 < yellowarea)
                        {
                            for (int i = 0; i < 14; i++)
                            {
                                LED[i].Red = true;
                                LED[i].Green = true;
                                LED[i].Blue = false;
                            }
                        }
                        if (anInteger2 >= yellowarea)
                        {
                            for (int i = 0; i < 14; i++)
                            {
                                LED[i].Green = true;
                                LED[i].Red = false;
                                LED[i].Blue = false;
                            }
                        }
                    }
                }
                LEDEvent();
            }

            private void TextBox_TextChanged_3(object sender, TextChangedEventArgs e)
            {
                float anInteger3;
                anInteger3 = Convert.ToSingle(textbox3.Text);
                float anInteger4;
                anInteger4 = Convert.ToSingle(textbox4.Text);
                double redarea = anInteger4 * 3600 / 2000;
                double yellowarea = anInteger4 * 1.75 + (long)Math.Pow(anInteger4, 2) / 20;
                if (textbox.Text == "2")
                {
                    for (int i = 45; i < 60; i++)
                    {
                        LED[i].Red = false;
                        LED[i].Green = false;
                        LED[i].Blue = false;
                    }
                    if (anInteger4 == 0)
                    {
                        if (anInteger3 < 2.5)
                        {
                            for (int i = 46; i < 60; i++)
                            {
                                LED[i].Red = true;
                                LED[i].Green = false;
                                LED[i].Blue = false;
                            }
                        }
                        else
                        {
                            for (int i = 46; i < 60; i++)
                            {
                                LED[i].Green = true;
                                LED[i].Red = false;
                                LED[i].Blue = false;
                            }
                        }
                    }
                    else
                    {
                        if (anInteger3 < redarea)
                        {
                            for (int i = 46; i < 60; i++)
                            {
                                LED[i].Red = true;
                                LED[i].Green = false;
                                LED[i].Blue = false;
                            }
                        }
                        if (anInteger3 >= redarea && anInteger3 < yellowarea)
                        {
                            for (int i = 46; i < 60; i++)
                            {
                                LED[i].Red = true;
                                LED[i].Green = true;
                                LED[i].Blue = false;
                            }
                        }
                        if (anInteger3 >= yellowarea)
                        {
                            for (int i = 46; i < 60; i++)
                            {
                                LED[i].Green = true;
                                LED[i].Red = false;
                                LED[i].Blue = false;
                            }
                        }
                    }
                }
                if (textbox.Text == "3")
                {
                    for (int i = 15; i < 60; i++)
                    {
                        LED[i].Red = false;
                        LED[i].Green = false;
                        LED[i].Blue = false;
                    }
                    if (anInteger4 == 0)
                    {
                        if (anInteger3 < 2.5)
                        {
                            for (int i = 16; i < 60; i++)
                            {
                                LED[i].Red = true;
                                LED[i].Green = false;
                                LED[i].Blue = false;
                            }
                        }
                        else
                        {
                            for (int i = 16; i < 60; i++)
                            {
                                LED[i].Green = true;
                                LED[i].Red = false;
                                LED[i].Blue = false;
                            }
                        }
                    }
                    else
                    {
                        if (anInteger3 < redarea)
                        {
                            for (int i = 16; i < 60; i++)
                            {
                                LED[i].Red = true;
                                LED[i].Green = false;
                                LED[i].Blue = false;
                            }
                        }
                        if (anInteger3 >= redarea && anInteger3 < yellowarea)
                        {
                            for (int i = 16; i < 60; i++)
                            {
                                LED[i].Red = true;
                                LED[i].Green = true;
                                LED[i].Blue = false;
                            }
                        }
                        if (anInteger3 >= yellowarea)
                        {
                            for (int i = 16; i < 60; i++)
                            {
                                LED[i].Green = true;
                                LED[i].Red = false;
                                LED[i].Blue = false;
                            }
                        }
                    }
                }
                LEDEvent();
            }

            private void textbox4_TextChanged(object sender, TextChangedEventArgs e)
            {
                
            }
        
        private  void TextBlock_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            
        }

        private void textblock2_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void TextBlock_SelectionChanged_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
