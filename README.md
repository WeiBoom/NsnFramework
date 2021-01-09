# NsnFramework For Unity

Created: Jul 14, 2020 11:57 PM
Tags: Framework, Unity

[**NSN.Framework](https://github.com/WeiBoom/NsnFramework)** 是一套提供给Unity开发者的开发框架，旨在做到开箱即用，提高开发效率。商业项目亦或是独立游戏，都可以做到开箱即用，让游戏开发变得更为简单。

**QQ：631450364**

**Unity 版本：2019.4.9f1**

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

![NsnFramework%20For%20Unity%20fab0a4b5cd604c689c66f60438b7fbbb/Untitled.png](NsnFramework%20For%20Unity%20fab0a4b5cd604c689c66f60438b7fbbb/Untitled.png)

Initialize会自动生成框架所需的配置文件（存放在Assets/NsnFramework/Resources/Setting/ 目录下），通过MenuInspetor可以直接查看生成的配置文件内容。

PS：框架内已经初始化好了配置文件，并且在Examples文件夹下，以及提供了一套使用规范示例。

## 框架核心内容

- **可配置的一键打包工具，支持jenkins。**

**可配置的资源模式是NsnFramework的核心，大部分内容都基于这种方法**。在不同的项目，几乎用编写不同的处理各种资源路径的打包和读取代码。NsnFramework提供了把所需要打包构建的资源文件用可配置的形式。每个文件下所需对应的资源类型，文件类型，打包命名格式等都通过配置来处理。资源存放和读取的路径也通过配置来获取，通过配置来适应不同项目的文件目录。

**注意：大部分项目都需要严格管理自己的资源，不仅是为了方便打包，也是为了更好的管理、优化项目。**

![NsnFramework%20For%20Unity%20fab0a4b5cd604c689c66f60438b7fbbb/Untitled%201.png](NsnFramework%20For%20Unity%20fab0a4b5cd604c689c66f60438b7fbbb/Untitled%201.png)

- **简单易上手的UI管理框架**

框架内提供了一套非常简单且易扩展的 UI管理架构，可以自动生成UI初始化代码，免去繁琐的transform.Find("ui").GetComponent<T>()的操作，支持C#与lua。自动生成的代码以及逻辑都放置在上面配置的文件夹中。

![NsnFramework%20For%20Unity%20fab0a4b5cd604c689c66f60438b7fbbb/Untitled%202.png](NsnFramework%20For%20Unity%20fab0a4b5cd604c689c66f60438b7fbbb/Untitled%202.png)

**C#代码示例，利用C# partical特性，开发者无需关注各个组件的获取操作。**

![NsnFramework%20For%20Unity%20fab0a4b5cd604c689c66f60438b7fbbb/Untitled%203.png](NsnFramework%20For%20Unity%20fab0a4b5cd604c689c66f60438b7fbbb/Untitled%203.png)

该部分代码自动生成，无需开发者管理

![NsnFramework%20For%20Unity%20fab0a4b5cd604c689c66f60438b7fbbb/Untitled%204.png](NsnFramework%20For%20Unity%20fab0a4b5cd604c689c66f60438b7fbbb/Untitled%204.png)

UI逻辑的具体实现，开发者自己编写逻辑

**Lua代码示例，Attribute 方法是用来获取组件，同C#一样，Panel是具体的逻辑实现，代码实现的功能和上面C#实现的功能是一样的。**

![NsnFramework%20For%20Unity%20fab0a4b5cd604c689c66f60438b7fbbb/Untitled%205.png](NsnFramework%20For%20Unity%20fab0a4b5cd604c689c66f60438b7fbbb/Untitled%205.png)

该代码自动生成，开发者无需关心内容，且仅有对应的Panel界面会require，其余不会也不应该来访问该部分代码。

![NsnFramework%20For%20Unity%20fab0a4b5cd604c689c66f60438b7fbbb/Untitled%206.png](NsnFramework%20For%20Unity%20fab0a4b5cd604c689c66f60438b7fbbb/Untitled%206.png)

包含自身组件的table，Ui逻辑由开发者自己实现。

- **资源加载**

框架内的AssetBunde的加载模式基于[**开源框架xasset**](https://github.com/xasset/xasset)而来，秉承xasset的理念，让开发者不再过多关心AssetBundle底层的东西，把更多的精力和事件用于游戏内容的创作中！

- **计时器**

提供延时，循环，自定义三种计时器处理方式。后续会更新的计时器算法。

- **AI**

框架内提供了简单的状态机和行为树解决方案。帮助梗快的构建自己的AI系统。

## 框架示例：

场景案例：Assets/NsnFramework/Examples/Scenes/ Example_Scene.unity

配置案例：Assets/NsnFramework/Resources/Setting/

![NsnFramework%20For%20Unity%20fab0a4b5cd604c689c66f60438b7fbbb/Untitled%207.png](NsnFramework%20For%20Unity%20fab0a4b5cd604c689c66f60438b7fbbb/Untitled%207.png)

NsnFramework 启动场景，UI加载案例（AssetBundle模式）

## 后续开发

曾有小伙伴这样提过：”大部分框架都提供了很多内容，然而我只想要其中部分，太多的内容让我根本看不过来，也用不上。“ Boom深以为然，所以在未来的开发中，NsnFramework会将大部分内容以扩展包的形式打包成unitypackage放在项目中，需要的同学们可以自行获取，刚接触的同学也可以只需要关注NsnFramework的核心内容。

**未来的扩展/开发内容：**

- [ ]  游戏内辅助工具
- [ ]  模型优化工具
- [ ]  UGUI组件扩展
- [ ]  Lua框架扩展
- [ ]  网络信息处理
- [ ]  And More ...

## **最后**

为什么叫NsnFramework? **Nsn = Nevery say never**。希望各位怀有梦想与希望的开发者，能坚守自己的理念，坚持自己的梦想，永不言弃。也希望小小的NsnFramework能在各位的道路上尽到一点绵薄之力！