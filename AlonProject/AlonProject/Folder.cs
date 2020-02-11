using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlonProject
{
    class Folder
    {
        #region Properties 

        /// <summary>
        /// The Full Path of the folder
        /// </summary>
        public string FullPath { get; private set; }

        /// <summary>
        /// The Folder Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The parent folder (1 level up in the hierarchi) 
        /// </summary>
        public Folder ParentFolder { get; private set; }

        /// <summary>
        /// All Files in the folder
        /// </summary>
        public List<string> Files { get; private set; }

        /// <summary>
        /// All Folders contained in this folder
        /// </summary>
        public Dictionary<string, Folder> SubFolders { get; private set; }



        #endregion Properties


        #region methods

        #region Constructors

        public Folder(string folderName) : this(null, folderName) { }

        // Initialize 
        public Folder(Folder parentFolder, string folderName)
        {
            Name = folderName;
            Files = new List<string>();
            SubFolders = new Dictionary<string, Folder>();
            ParentFolder = parentFolder;
            FullPath = parentFolder != null ? string.Format("{0}\\{1}", parentFolder.FullPath, Name) : Name;
        }

        #endregion Constructors

        /// <summary>
        /// Creates a subfolders
        /// </summary>
        /// <param name="folderName">The name of folder that you want to create</param>
        /// <exception cref="System.Exception">Can Throw exception if folder already exists</exception>
        public void CreateFolder(string folderName)
        {
            Regex regexItem = new Regex("^[a-zA-Z0-9 ]*$");

            if (!regexItem.IsMatch(folderName))
            {
                throw new Exception(string.Format("The folder {0} should contain only alfa numeric charecters only", folderName));
            }

            if (SubFolders.ContainsKey(folderName))
            {
                throw new Exception(string.Format("The folder {0} is already exists", folderName));
            }

            // Add subfolders to dictionary
            SubFolders.Add(folderName, new Folder(this, folderName));
        }

        /// <summary>
        /// Return a specific <seealso cref="Folder"/> contained in this folder by folder name
        /// </summary>
        /// <param name="subFolder"> the sub folder name to return</param>
        /// <returns>The sub folder</returns>
        /// <exception cref="KeyNotFoundException" > If cannot find the folder</exception>
        public Folder GetSubFolder(string subFolder)
        {
            if (string.IsNullOrEmpty(subFolder) || !SubFolders.ContainsKey(subFolder))
            {
                throw new KeyNotFoundException(string.Format("The folder {0} does not exist.", subFolder));
            }

            return SubFolders[subFolder];
        }

        /// <summary>
        /// Creates a file
        /// </summary>
        /// <param name="fileName">File name to create</param>
        /// <exception cref="System.Exception">Can Throw exception if file already exists</exception>
        public void CreateFile(string fileName)
        {
            if (Files.Contains(fileName))
            {
                throw new System.Exception(string.Format("The file {0} is already exists", fileName));
            }

            Files.Add(fileName);
        }

        /// <summary>
        /// List of Folder content
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string sf in SubFolders.Keys)
            {
                sb.AppendLine(sf);
            }

            foreach (string f in Files)
            {
                sb.AppendLine(f);
            }

            return sb.ToString();
        }


        #endregion Methods

    }
}
