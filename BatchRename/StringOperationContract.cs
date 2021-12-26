﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;

namespace BatchRename
{
    public class StringArgs
    {
    }

    public class ChangeExtensionArgs : StringArgs, INotifyPropertyChanged
    {
        public string From { get; set; }
        public string To { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class NewCaseArgs : StringArgs
    {
        public int style { get; set; } //1: All Uppercase, 2: All lowercase, 3: UpperCase the first character each word
    }

    public class RemovePatternArgs : StringArgs
    {
        public string pattern { get; set; }
    }

    public class TrimArgs : StringArgs
    {
    }

    public class FullnameNormalizeArgs : StringArgs
    {
    }

    public class AddPrefixArgs : StringArgs
    {
        public string text { get; set; }
    }

    public class AddSuffixArgs : StringArgs
    {
        public string text { get; set; }
    }

    public class ReplaceSpaceToDotArg : StringArgs
    {
    }

    public abstract class StringMethod : INotifyPropertyChanged
    {
        public StringArgs Args { get; set; }
        public virtual string Name { get; set; }
        public virtual string Error { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public abstract string Operate(string origin, bool isFileName);

        public abstract StringMethod Clone();

        public abstract void Config();

        public virtual string Description { get; set; }
    }

    public class ChangeExtensionMethod : StringMethod, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Name => "Change extension";

        public override string Description
        {
            get
            {
                var args = Args as ChangeExtensionArgs;
                return $"Replace extension '{args.From}' to '{args.To}' ";
            }
        }

        public override StringMethod Clone()
        {
            var oldArgs = Args as ChangeExtensionArgs;
            return new ChangeExtensionMethod()
            {
                Args = new ChangeExtensionArgs()
                {
                    From = oldArgs.From,
                    To = oldArgs.To,
                }
            };
        }

        public override string Operate(string origin, bool isFileName)
        {
            var args = Args as ChangeExtensionArgs;
            var from = args.From;
            var to = args.To;

            if (isFileName)
            {
                string name = Path.GetFileNameWithoutExtension(origin);
                string extension = Path.GetExtension(origin);

                extension = extension.Replace(from, to);

                return (name + extension);
            }
            return origin.Replace(from, to);
        }

        public override void Config()
        {
            var screen = new ChangeExtensionMethodControl(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }
        }
    }

    public class NewCaseMethod : StringMethod, INotifyPropertyChanged
    {
        public override string Name => "New Case";

        public event PropertyChangedEventHandler PropertyChanged;

        public override StringMethod Clone()
        {
            var oldArgs = Args as NewCaseArgs;
            return new NewCaseMethod()
            {
                Args = new NewCaseArgs()
                {
                    style = oldArgs.style
                }
            };
        }

        public override void Config()
        {
            var screen = new NewCaseMethodControl(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }
        }

        public override string Operate(string origin, bool isFileName)
        {
            var args = Args as NewCaseArgs;
            if (isFileName == true)
            {
                string name = Path.GetFileNameWithoutExtension(origin);
                string extension = Path.GetExtension(origin);
                if (args.style == 1)
                {
                    name = name.ToUpper();
                }
                else if (args.style == 2)
                {
                    name = name.ToLower();
                }
                else
                {
                    name = name.ToLower();
                    TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
                    name = cultInfo.ToTitleCase(name);
                }
                return (name + extension);
            }
            else
            {
                if (args.style == 1)
                {
                    return origin.ToUpper();
                }
                else if (args.style == 2)
                {
                    return origin.ToLower();
                }
                else
                {
                    origin = origin.ToLower();
                    TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
                    string res = cultInfo.ToTitleCase(origin);
                    return res;
                }
            }
        }

        public override string Description
        {
            get
            {
                var args = Args as NewCaseArgs;

                if (args.style == 1)
                {
                    return "Uppercase all letters";
                }
                if (args.style == 2)
                {
                    return "Lowercase all letters";
                }
                if (args.style == 3)
                {
                    return "Only Uppercase the first letter of each word";
                }
                return "error";
            }
        }
    }

    public class RemovePatternMethod : StringMethod, INotifyPropertyChanged
    {
        public override string Name => "Remove Pattern";

        public event PropertyChangedEventHandler PropertyChanged;

        public override StringMethod Clone()
        {
            var oldArgs = Args as RemovePatternArgs;
            return new RemovePatternMethod()
            {
                Args = new RemovePatternArgs()
                {
                    pattern = oldArgs.pattern
                }
            };
        }

        public override void Config()
        {
            throw new NotImplementedException();
        }

        public override string Operate(string origin, bool isFileName)
        {
            var args = Args as RemovePatternArgs;
            return origin.Replace(args.pattern, "");
        }
    }

    public class TrimMethod : StringMethod, INotifyPropertyChanged
    {
        public override string Name => "Trim";

        public event PropertyChangedEventHandler PropertyChanged;

        public override StringMethod Clone()
        {
            var oldArgs = Args as TrimArgs;
            return new TrimMethod()
            {
                Args = new TrimArgs()
                {
                }
            };
        }

        public override void Config()
        {
            MessageBox.Show("Dont't need to config this method");
        }

        public override string Operate(string origin, bool isFileName)
        {
            return origin.Trim();
        }

        public override string Description
        {
            get
            {
                return "Removes all beginning and ending white-space characters";
            }
        }
    }

    public class FullnameNormalizeMethod : StringMethod, INotifyPropertyChanged
    {
        public override string Name => "Fullname Normalize";

        public event PropertyChangedEventHandler PropertyChanged;

        public override StringMethod Clone()
        {
            var oldArgs = Args as FullnameNormalizeArgs;
            return new FullnameNormalizeMethod()
            {
                Args = new FullnameNormalizeArgs()
                {
                }
            };
        }

        public override void Config()
        {
            MessageBox.Show("Don't need to config this method");
        }

        public override string Operate(string origin, bool isFileName)
        {
            if (isFileName)
            {
                string name = Path.GetFileNameWithoutExtension(origin);
                string extension = Path.GetExtension(origin);
                //Viet hoa cac chu cai dau cua moi tu
                name = name.ToLower();
                TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
                name = cultInfo.ToTitleCase(name);

                //Xoa bo cac khoang trang dau va cuoi
                name = name.Trim();

                //Xoa bo cac khoang trang thua
                name = System.Text.RegularExpressions.Regex.Replace(name, @"\s+", " ");

                return (name + extension);
            }
            else
            {
                origin = origin.ToLower();
                TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
                origin = cultInfo.ToTitleCase(origin);

                //Xoa bo cac khoang trang dau va cuoi
                origin = origin.Trim();

                //Xoa bo cac khoang trang thua
                origin = System.Text.RegularExpressions.Regex.Replace(origin, @"\s+", " ");

                return origin;
            }
        }

        public override string Description
        {
            get
            {
                return "Remove all extra white spaces and Uppercase the first letter of each word";
            }
        }
    }

    public class ReplaceSpaceToDotMethod : StringMethod, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Name => "Replace Space to Dot";

        public override string Description
        {
            get
            {
                var args = Args as ReplaceSpaceToDotArg;

                return $"Replace  ' ' to '.'";
            }
        }

        public override StringMethod Clone()
        {
            var oldArgs = Args as ReplaceSpaceToDotArg;
            return new ReplaceSpaceToDotMethod()
            {
                Args = new ReplaceSpaceToDotArg()
                {
                }
            };
        }

        public override void Config()
        {
            MessageBox.Show("Dont't need to config this method");
        }

        public override string Operate(string origin, bool isFileName)
        {
            var args = Args as ReplaceSpaceToDotArg;

            if (isFileName)
            {
                string name = Path.GetFileNameWithoutExtension(origin);
                string extension = Path.GetExtension(origin);
                name = name.Replace(" ", ".");
                return (name + extension);
            }
            return origin;
        }
    }

    public class AddPrefixMethod : StringMethod, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Name => "Add Prefix";

        public override string Description
        {
            get
            {
                var args = Args as AddPrefixArgs;

                return $"AddPrefix  '{args.text}'";
            }
        }

        public override StringMethod Clone()
        {
            var oldArgs = Args as AddPrefixArgs;
            return new AddPrefixMethod()
            {
                Args = new AddPrefixArgs()
                {
                    text = oldArgs.text,
                }
            };
        }

        public override void Config()
        {
            var screen = new AddPrefixMethodControl(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }
        }

        public override string Operate(string origin, bool isFileName)
        {
            var args = Args as AddPrefixArgs;
            var text = args.text;

            if (isFileName)
            {
                string name = Path.GetFileNameWithoutExtension(origin);
                string extension = Path.GetExtension(origin);
                name = text + name;
                return (name + extension);
            }
            return origin;
        }
    }

    public class AddSuffixMethod : StringMethod, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Name => "Add Suffix";

        public override string Description
        {
            get
            {
                var args = Args as AddSuffixArgs;

                return $"AddSuffix  '{args.text}'";
            }
        }

        public override StringMethod Clone()
        {
            var oldArgs = Args as AddSuffixArgs;
            return new AddSuffixMethod()
            {
                Args = new AddSuffixArgs()
                {
                    text = oldArgs.text,
                }
            };
        }

        public override void Config()
        {
            var screen = new AddSuffixMethodControl(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
            }
        }

        public override string Operate(string origin, bool isFileName)
        {
            var args = Args as AddSuffixArgs;
            var text = args.text;

            if (isFileName)
            {
                string name = Path.GetFileNameWithoutExtension(origin);
                string extension = Path.GetExtension(origin);
                name = name + text;
                return (name + extension);
            }
            return origin;
        }
    }
}