# NsnFramework For Unity

Created: Jul 14, 2020 11:57 PM
Tags: Framework, Unity

 NSN.Framework是一套提供给Unity开发者的开发框架，旨在做到开箱即用，提高开发效率。商业项目亦或是独立游戏，都可以做到开箱即用，让游戏开发变得更为简单。

QQ：631450364

## 辅助插件

- **Odin Inspector**

    **[Odin Inspector](https://assetstore.unity.com/packages/tools/utilities/odin-inspector-and-serializer-89041)**是一款非常优秀的编辑器插件，在做各种辅助工具以及编辑器方面，我愿称之为最强！曾经做编辑器时也很倔强的要自己写，虽然实现了部分功能，但是花费时间精力太久，而且代码显得臃肿复杂，不易扩展，也不易维护，Odin做到了开箱即用，应有尽有，是每个Unity项目的不二之选！**PS：因为Odin属于付费插件，所以提供的框架不包含Odin插件，需要自行导入。**

- **DOTween**

    DOTween是一款针对Unity的补间动画插件，兼顾快速与高效，在代码上也做到了直观、易用的特性，DOTween的pro版本提供了可视化编辑的功能，更方便开发者调试、编辑动画。框架内包含了Unity AssetStore提供的**免费版的DOTween**，有需要可以去**[Unity资源商店](https://assetstore.unity.com/)**或[**DOTween官网**](http://dotween.demigiant.com/)了解更多。

## 安装/导入 NSN框架

**通过 git URL**

2019.3.4f1以及以上版本支持使用git url安装。Unity-Window-PackageManager-Advance-Git url，

填入项目的git地址，即可导入安装。

**通过 *.unitypackage**

通过Release发布的unitypackage文件导入安装。

## 启用框架：

![README_Image/Untitled.png](README_Image/Untitled.png)

Initialize会自动生成框架所需的配置文件（存放在Assets/NsnFramework/Resources/Setting/ 目录下），通过MenuInspetor可以直接查看生成的配置文件内容。

PS：框架内已经初始化好了配置文件，并且在Examples文件夹下，以及提供了一套使用规范示例。

## 框架内容

- **可配置的一键打包工具，支持jenkins。**

**可配置的资源模式是NsnFramework的核心，大部分内容都基于这种方法**。在不同的项目，几乎用编写不同的处理各种资源路径的打包和读取代码。NsnFramework提供了把所需要打包构建的资源文件用可配置的形式。每个文件下所需对应的资源类型，文件类型，打包命名格式等都通过配置来处理。资源存放和读取的路径也通过配置来获取，通过配置来适应不同项目的文件目录。

**注意：大部分项目都需要严格管理自己的资源，不仅是为了方便打包，也是为了更好的管理、优化项目。**

![README_Image/Untitled%201.png](README_Image/Untitled%201.png)

- **简单易上手的UI管理框架**

框架内提供了一套非常简单且易扩展的 UI管理架构，可以自动生成UI初始化代码，免去繁琐的transform.Find("ui").GetComponent<T>()的操作，支持C#与lua。自动生成的代码以及逻辑都放置在上面配置的文件夹中。

![README_Image/Untitled%202.png](README_Image/Untitled%202.png)

- **资源加载**

框架内的AssetBunde的加载模式基于[**开源框架xasset**](https://github.com/xasset/xasset)而来，秉承xasset的理念，让开发者不再过多关心AssetBundle底层的东西，把更多的精力和事件用于游戏内容的创作中！

## 框架示例：

场景案例：Assets/NsnFramework/Examples/Scenes/ Example_Scene.unity

配置案例：Assets/NsnFramework/Resources/Setting/

![README_Image/Untitled%203.png](README_Image/Untitled%203.png)

NsnFramework 启动场景，UI加载案例（AssetBundle模式）

## 后续开发

曾有小伙伴这样提过：”大部分框架都提供了很多内容，然而我只想要其中部分，太多的内容让我根本看不过来，也用不上。“ Boom深以为然，所以在未来的开发中，NsnFramework会将大部分内容以扩展包的形式打包成unitypackage放在项目中，需要的同学们可以自行获取，刚接触的同学也可以只需要关注NsnFramework的核心内容。

**未来的扩展内容：**

- [ ]  Network
- [ ]  UGUI Component Extend
- [ ]  Tools
- [ ]  Fix bugs
- [ ]  And More ...

## **最后**

为什么叫NsnFramework? **Nsn = Nevery say never**。希望各位怀有梦想与希望的开发者，能坚持理念与理想，永不言弃，创作出更优秀的“第九艺术”。也希望小小的NsnFramework能在各位的道路上尽到一点绵薄之力！
