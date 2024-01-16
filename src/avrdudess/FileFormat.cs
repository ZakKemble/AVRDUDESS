// AVRDUDESS - A GUI for AVRDUDE
// https://blog.zakkemble.net/avrdudess-a-gui-for-avrdude/
// https://github.com/ZakKemble/AVRDUDESS
// Copyright (C) 2013-2024, Zak Kemble
// GNU GPL v3 (see License.txt)

namespace avrdudess
{
    class FileFormat
    {
        public string Id { get; private set; }
        public string Desc { get; private set; }

        public FileFormat(string id, string desc)
        {
            Id = id;
            Desc = desc;
        }

        public void ApplyTranslation()
        {
            Desc = Language.Translation.get(Desc);
        }
    }
}
