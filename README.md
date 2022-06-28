# NsnFramework For Unity

**QQ：631450364**

**Unity 版本：2019.4.28f1**

## 辅助插件

- **Odin Inspector ——** 编辑器的神 YYDS。**PS：付费插件，需要自行导入。**
- **DOTween ——** 框架内包含了Unity AssetStore提供的**免费版的DOTween**

## 启用框架：

![README_Image/Untitled.png](README_Image/Untitled.png)

Initialize会自动生成框架所需的配置文件（存放在Assets/NsnFramework/Resources/Setting/ 目录下），通过MenuInspetor可以直接查看生成的配置文件内容。

PS：框架内已经初始化好了配置文件，并且在**Examples文件夹下，提供了一套使用示例**。

## 打包配置示例

**可配置的一键打包工具，支持jenkins。**

**可配置的资源模式是NsnFramework的核心，大部分内容都基于这种方法**。在不同的项目，几乎用编写不同的处理各种资源路径的打包和读取代码。NsnFramework提供了把所需要打包构建的资源文件用可配置的形式。每个文件下所需对应的资源类型，文件类型，打包命名格式等都通过配置来处理。资源存放和读取的路径也通过配置来获取，通过配置来适应不同项目的文件目录。

**注意：大部分项目都需要严格管理自己的资源，不仅是为了方便打包，也是为了更好的管理、优化项目。**

![README_Image/Untitled%201.png](README_Image/Untitled%201.png)

## **UI 示例**

UI Inspector面板，**自动获取**Button、Texture等指定节点组件，生成C#或Lua 匹配的UI代码

![README_Image/Untitled%202.png](README_Image/Untitled%202.png)

**C#代码示例，利用C# partical特性，开发者无需关注各个组件的获取操作。**

![README_Image/Untitled%203.png](README_Image/Untitled%203.png)

该部分代码自动生成，无需开发者管理

![README_Image/Untitled%204.png](README_Image/Untitled%204.png)

UI逻辑的具体实现，开发者自己编写逻辑

**Lua代码示例，Attribute 方法是用来获取组件，同C#一样，Panel是具体的逻辑实现，代码实现的功能和上面C#实现的功能是一样的。**

![README_Image/Untitled%205.png](README_Image/Untitled%205.png)

该代码自动生成，开发者无需关心内容，且仅有对应的Panel界面会require，其余不会也不应该来访问该部分代码。

![README_Image/Untitled%206.png](README_Image/Untitled%206.png)

包含自身组件的table，Ui逻辑由开发者自己实现。

## 框架示例：

场景案例：Assets/NsnFramework/Examples/Scenes/ Example_Scene.unity

配置案例：Assets/NsnFramework/Resources/Setting/

![README_Image/Untitled%207.png](README_Image/Untitled%207.png)

NsnFramework 启动场景，UI加载案例（AssetBundle模式）