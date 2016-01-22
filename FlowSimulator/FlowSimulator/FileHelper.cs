using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace FlowSimulator
{
    [Serializable()]
    /// <summary>
    /// The purpose of this class is to assist with the opening and saving of a canvas and its components.
    /// </summary>
    public class FileHelper
    {
        FileStream fs;
        BinaryFormatter bf;
        private SaveFileDialog saveCanvas = new SaveFileDialog();
        private OpenFileDialog openCanvas = new OpenFileDialog();
        /// <summary>
        /// The name of the file to be opened or saved
        /// </summary>
        public string Filename
        {
            get;
            set;
        }
    
        /// <summary>
        /// To save the components on a canvas and its properties to file
        /// </summary>
        public bool SaveToFile(Canvas canvas)
        {
            bf = null;
            fs = null;
                saveCanvas.FileName = "UntitledCanvas.cnv";
                saveCanvas.Filter = "Canvas file(*.cnv)|*.cnv";
                Filename = saveCanvas.FileName;
                DialogResult dr = saveCanvas.ShowDialog();
                if (saveCanvas.FileName != "*.cnv")
                {
                    
                    try
                    {

                        if (dr.ToString() == "OK")
                        {
                            fs = new FileStream(saveCanvas.FileName, FileMode.Create, FileAccess.Write);
                            bf = new BinaryFormatter();
                            bf.Serialize(fs, canvas);
                            return true;
                        }
                    }
                    catch (SerializationException)
                    { }

                    finally
                    {
                        if (fs != null) fs.Close();
                    }
                
            }
            return false;
        }

        /// <summary>
        /// To open a saved canvas into the flow network simulation system
        /// </summary>

        public Canvas LoadFromFile()
        {
            bf = null;
            fs = null;
            try
            {
                DialogResult dr = openCanvas.ShowDialog();
                if (dr.ToString() == "OK")
                {
                    fs = new FileStream(openCanvas.FileName, FileMode.Open, FileAccess.Read);
                    bf = new BinaryFormatter();
                    Filename = openCanvas.FileName;
                    fs.Position = 0;
                   Canvas c = ((Canvas)(bf.Deserialize(fs)));
                   return c;
                }
            }
            catch (SerializationException) { }
            finally
            {
                if (fs != null) fs.Close();
            }
            return null;
        }
    }
}
