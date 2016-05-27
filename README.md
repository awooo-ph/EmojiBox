# EmojiBox
A .NET library to quickly integrate an emoji picker/text box into your WPF app!

While developing my calendar program, [Shine Calendar](http://shinecalendar.tumblr.com/), I decided I wanted to add emoji support to it so that users could create events and use emoji to share with it. I soon quickly realized that WPF does not include any way of handling emoji out of the box. *(The best you can do is use the Segoe UI Symbols font, but it only gives you monochrome images that, frankly, don't look all that great.)* **So, I decided to build my own emoji support!**

EmojiBox provides a relatively simple and quick-to-set-up process to add Emoji support to your app!

EmojiBox uses [Emoji One](http://emojione.com) as the emoji image source! EmojiBox also uses the easy-to-use [Json.NET](http://www.newtonsoft.com/json) library.

### Start using EmojiBox

Soon enough, you'll see this text replaced with a quick-and-easy 4-step guide to get yourself going with EmojiBox. In the meantime, though, I'm still developing and building things! Come back once we're in a good enough state!

### Start developing EmojiBox

EmojiBox is built using Visual Studio 2015, and targets .NET Framework 4.5. If you'd like to download the code and start working away, go ahead!

You're able to submit pull requests at any time, but do be sure to be descriptive in your pull request (if not your code). If you're creating new publicly-accessible methods or classes, please do be sure to also create [proper XML documentation](https://msdn.microsoft.com/en-us/library/z04awywx.aspx) for them!

### Licensing / About

EmojiBox is available under the [MIT License](license.txt), meaning that you can use this code (and the DLL) in any way you want, *including commercial use*.

Emoji images provided by [Emoji One library](http://emojione.com). Additional emoji handling by [Json.NET Framework](http://www.newtonsoft.com/json).

EmojiBox was created by Jacob R. Huempfner, and is maintained by Jacob R. Huempfner and the Shine Development Group.

[Licensing FAQs](licensing-faqs.md)

### FAQs

1. What should I do about Emoji support for Universal Windows app (UWP) projects?

  * You're in luck! Emoji are supported out of the box, without any special handling. If you insert emojji from your (touch) keyboard, it'll appear in the app like magic. This, sadly, is not the same case with WPF. Typing emoji into WPF apps will just give you the monochrome Segoe UI Symbols images.
  
2. What about Emoji support for Windows Forms?

  * Unfortunately, that's outside the scope of this project. Windows Forms is great, but support for it was not an immediate need that I had while developing this. If you want to use some of my code as a starting point for creating a library for Windows Forms, go ahead!
  
3. Will this be available as an easy download, and/or on NuGet?

  * Yes, once we're at a good enough state.
  
4. Why not support .NET Framework 4.0? Or .NET Framework 3.5?

  * This was not an immediate need that I had while making this. This code will run just fine on .NET Framework 4.0, and should also have no issues with .NET Framework 3.5. I can go and make separate projects/DLLs for .NET 4.0 support in the future. I've not touched .NET 3.5 in years, though, and don't even have it on my development machine, so if you'd like to create a fork to add support for it, go ahead!
  * That being said, [I do suggest upgrading to .NET Framework 4.5 though](https://msdn.microsoft.com/en-us/library/ms171868%28v=vs.110%29.aspx#core) if you're able! (I'd imagine, though, that those of you still on .NET 3.5 probably have a specific reason for doing so.)
  
5. Why is the DLL so big?

  * The compiled DLL includes the entire Emoji One image library within it as resource data (excepting the 512 by 512 images, because those are massive). I know the resulting DLL is a bit big, but honestly, you can't really have an emoji picker if you don't have emoji...
 
6. Why is the Json.NET DLL needed? Can you remove the library from your code?

  * To perform a lot of its functions, EmojiBox relies upon a JSON file that is included with the Emoji One library. I decided to use the Json.NET library to make working with the JSON data easy; I know the library is probably a bit overkill for what I need it for, and in the future, I'll probably look into other ways to read the data instead. Having another DLL in your program's folder won't kill you though. Honestly, chances are you're using that library for something else already anyway!

7. Where can I documentation or samples?

  * The code includes a Sample library that you can get insipiration from (or just simply copy over a bunch of code from). That should be enough to provide a bit of a jump-start, but if people have more ideas of other samples to make, let me know!
  * I will also get the documentation up some point soon!
  
8. I have a licensing question.

  * Check the [Licensing FAQs](licensing-faqs.md), or contact me!

### Contact Me

I can be reached on Twitter [@JaykeBird](http://twitter.com/JaykeBird). If you need to send an email, you can do so at shine-calendar@outlook.com.
