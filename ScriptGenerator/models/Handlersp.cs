namespace ScriptGenerator.models
{
    public class Handleresp
    {
        public string FilesPath { get; set; }
        public bool IsSucess { get; set; }

        public Handleresp()
        {
        }

        public Handleresp(string filesPath, bool isSucess)
        {
            FilesPath = filesPath;
            IsSucess = isSucess;
        }
    }
}