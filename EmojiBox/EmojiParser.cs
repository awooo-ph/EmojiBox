using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;
using System.Windows.Media.Imaging;
using System.Diagnostics;

namespace EmojiBox
{
    public class EmojiParser
    {

        private Dictionary<string, Emoji> emojis = new Dictionary<string, Emoji>();

        public EmojiParser()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string jsonFile = "EmojiBox.emoji.json";

            using (Stream s = assembly.GetManifestResourceStream(jsonFile))
            {
                JsonTextReader jtr = new JsonTextReader(new StreamReader(s));
                JsonSerializer js = new JsonSerializer();

                while (jtr.Read())
                {
                    emojis = js.Deserialize<Dictionary<string, Emoji>>(jtr);
                }
            }
        }

        public List<string> ListEmojisWithUnicodes()
        {
            List<string> elist = new List<string>();

            foreach (KeyValuePair<string, Emoji> item in emojis)
            {
                elist.Add(item.Key + " (" + item.Value.Unicode + ")");
            }

            return elist;
        }

        Emoji Find(string name, bool useShortNameToo = true)
        {
            Emoji em = null;
            foreach (Emoji item in emojis.Values)
            {
                if (item.Name == name || (useShortNameToo && item.ShortName == (":" + name + ":")))
                {
                    em = item;
                    return em;
                }
            }

            if (em == null)
            {
                throw new KeyNotFoundException("There is not an emoji by the name \"" + name + "\".");
            }

            return em;
        }

        /// <summary>
        /// Gets or sets whether large emoji images should be returned.
        /// <para />
        /// If <code>true</code>, emoji images will be 128 x 128 in size. If <code>false</code>, emoji images will be 64 x 64 in size. (Height by width)
        /// </summary>
        public bool UseLargeImages { get; set; } = false;

        BitmapImage GetEmojiImage(Emoji em)
        {
            try
            {
                if (UseLargeImages)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/EmojiBox;component/Emoji/128/" + em.Unicode + ".png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    return new BitmapImage(new Uri("pack://application:,,,/EmojiBox;component/Emoji/64/" + em.Unicode + ".png", UriKind.RelativeOrAbsolute));
                }
            }
            catch (IOException e)
            {
                throw new ArgumentException("Cannot find an emoji with the name \"" + em.Name + "\" (Unicode symbol \"" + em.Unicode + "\").", nameof(em), e);
            }
        }

        /// <summary>
        /// Get a BitmapImage of an emoji with the specified name.
        /// </summary>
        /// <param name="name">The name of the emoji.</param>
        /// <exception cref="ArgumentException">Thrown if there is not an emoji by the specified name.</exception>
        /// <returns>A BitmapImage of the specified emoji. Unless <see cref="UseLargeImages"/> is set, the image will be 64 x 64 in size (height by width). </returns>
        public BitmapImage GetEmojiWithName(string name)
        {
            try
            {
                return GetEmojiImage(Find(name, true));
            }
            catch (KeyNotFoundException e)
            {
                throw new ArgumentException("There is not an emoji by the name \"" + name + "\".", nameof(name), e);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("There is not an emoji by the name \"" + name + "\".", nameof(name), e);
            }
        }

        /// <summary>
        /// Get a BitmapImage of an emoji with the specified Unicode symbol.
        /// </summary>
        /// <param name="unicode">The unicode symbol. Do not include any leading marks like "#" or "\u".</param>
        /// <exception cref="ArgumentException">Thrown if there is not an emoji with the specified Unicode symbol.</exception>
        /// <returns>A BitmapImage of the specified emoji. Unless <see cref="UseLargeImages"/> is set, the image will be 64 x 64 in size (height by width). </returns>
        public BitmapImage GetEmojiWithUnicode(string unicode)
        {
            try
            {
                if (UseLargeImages)
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Emoji/128/" + unicode + ".png"));
                }
                else
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Emoji/64/" + unicode + ".png"));
                }
            }
            catch (IOException e)
            {
                throw new ArgumentException("Cannot find an emoji with the Unicode symbol \"" + unicode + "\".", nameof(unicode), e);
            }
        }

    }
}
