using System;
using System.IO;

namespace ExecutedModulesJSON.Core
{
    class FileLoader

    {
        readonly private string _workingDirectory = null;
        readonly private string _projectDirectory = null;
        public string _pathToFile { get; }

        public FileLoader(string fileName)
        {
            _workingDirectory = Environment.CurrentDirectory;                           //  \ExecutedModulesJSON\bin\Debug
            _projectDirectory = Directory.GetParent(_workingDirectory).Parent.FullName; //  \ExecutedModulesJSON
            _pathToFile = _projectDirectory + $@"\{fileName}";                          //  \ExecutedModulesJSON\modules.json
        }
    }
}
