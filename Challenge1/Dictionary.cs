using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Challenge1
{
    #region Attributes
    /// <summary>
    /// The Translation table entity class.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Struct |
        AttributeTargets.Class |
        AttributeTargets.Constructor |
        AttributeTargets.Field |
        AttributeTargets.Method |
        AttributeTargets.Property, AllowMultiple = true)]
    public class CTranslationTable : System.Attribute
    {
        #region Inner class Translate
        /// <summary>
        /// Translation propertie.
        /// </summary>
        public class CTranslation
        {
            /// <summary>
            /// The reference code.
            /// </summary>
            public string InCode { get; set; }

            /// <summary>
            /// The Aptra Vision code.
            /// </summary>
            public string OutCode { get; set; }

            /// <summary>
            /// Determines whether the specified System.Object is equal to the current System.Object.
            /// </summary>
            /// <param name="obj">The System.Object to compare with the current System.Object.</param>
            /// <returns>true if the specified System.Object is equal to the current System.Object; otherwise, false.</returns>
            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                CTranslation objTranslate = obj as CTranslation;
                if (objTranslate == null) return false;
                return Equals(objTranslate);
            }

            /// <summary>
            /// Determines whether the specified Translate object is equal to the current Translate object.
            /// </summary>
            /// <param name="t">The Translate object to compare with the current Translate object.</param>
            /// <returns>true if the specified Translate.InCode is equal to the current Translate.InCode; otherwise, false.</returns>
            public bool Equal(CTranslation t)
            {
                if (t == null) return false;
                return this.InCode.Equals(t.InCode);
            }

            /// <summary>
            /// Translation constructor.
            /// </summary>
            public CTranslation() { }

            /// <summary>
            /// Translation constructor with parameters.
            /// </summary>
            /// <param name="inCode">The reference code.</param>
            /// <param name="outCode">The Aptra Vision code.</param>
            public CTranslation(string inCode, string outCode)
            {
                this.InCode = inCode;
                this.OutCode = outCode;
            }

            /// <summary>
            /// Serves as a hash function for a particular type.
            /// </summary>
            /// <returns>A hash code for the current System.Object.</returns>
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }
        #endregion

        /// <summary>
        /// The translation table name reference.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The translation items.
        /// </summary>
        public IList<CTranslation> Translations { get; set; }

        /// <summary>
        /// Convert from a list string[] to a list CTranslation.
        /// </summary>
        /// <param name="list">A list string[] item.</param>
        /// <returns>The new object with the list items.</returns>
        public static IList<CTranslation> ConvertTo(IList<string[]> list)
        {
            if (list == null || list.Count < 1 || list[0].Length < 3)
                return null;

            List<CTranslation> converted = new List<CTranslation>();

            foreach (string[] line in list)
                converted.Add(new CTranslation(line[0], line[1]));

            return converted;
        }
    }
    #endregion

    public class Dictionary
    {
        #region Singleton
        /// <summary>
        /// Dictionary Singleton
        /// </summary>
        public static Dictionary Instance
        {
            get
            {
                return instance;
            }
        }
        private static readonly Dictionary instance = new Dictionary();
        private Dictionary() { }
        #endregion

        #region Properties
        /// <summary>
        /// Configuration file
        /// </summary>
        string ConfigJSon { get; set; }

        /// <summary>
        /// The formated translation table
        /// </summary>
        public IList<CTranslationTable> TranslationTables { get; set; }
        #endregion

        #region Loading
        /// <summary>
        /// Load the setting file and the translation table file as a text.
        /// </summary>
        /// <returns>True if all files was loaded successfully.</returns>
        private bool LoadFiles()
        {
            //Console.WriteLine("LoadFiles;Config files: " + System.AppDomain.CurrentDomain.BaseDirectory +
            //        Properties.Settings.Default.ConfigFile);
            try
            {
                System.IO.StreamReader FileReader = new System.IO.StreamReader(
                    System.AppDomain.CurrentDomain.BaseDirectory + "\\" +
                    Properties.Settings.Default.ConfigFile);
                ConfigJSon = FileReader.ReadToEnd();
                FileReader.Close();

                return true;
            }
            catch (System.IO.IOException ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }

        /// <summary>
        /// Load readers, writers and code translation table entities.
        /// </summary>
        /// <returns>True if the settings was successfully loaded.</returns>
        public bool Load()
        {
            try
            {
                if (LoadFiles())
                {
                    JObject oConfig = JObject.Parse(ConfigJSon);
                    TranslationTables = ((JArray)oConfig[Properties.Settings.Default.ConfigKey][Properties.Settings.Default.TranslationKey]).
                        ToObject<IList<CTranslationTable>>();

                    oConfig = null;
                    return true;
                }
                else Console.WriteLine("Load;Its not possible to load the settings files.");
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine(ex);
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }
        #endregion
    }
}
