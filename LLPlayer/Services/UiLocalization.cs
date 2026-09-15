using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace LLPlayer.Services;

/// <summary>
/// 在不改变界面结构的前提下，为应用内固定文案提供本地化。
/// </summary>
public static class UiLocalization
{
    private static bool _initialized;
    private static UiLanguage _language;

    private static readonly IReadOnlyDictionary<string, string> Chinese = new Dictionary<string, string>(StringComparer.Ordinal)
    {
        ["About"] = "关于",
        ["Action"] = "操作",
        ["Add"] = "添加",
        ["Alignment"] = "对齐方式",
        ["Always On Top"] = "窗口置顶",
        ["Apply"] = "应用",
        ["Aspect Ratio"] = "画面比例",
        ["ASR"] = "语音识别",
        ["ASR settings"] = "语音识别设置",
        ["Audio"] = "音频",
        ["Audio Frames Max"] = "音频帧最大数",
        ["Audio Language (Priority to the above)"] = "音频语言（优先于上项）",
        ["Audio language can be set manually. If automatic detection is used, it will take precedence."] = "可手动设置音频语言；若使用自动检测，将优先采用自动检测结果。",
        ["Audio Streams"] = "音频流",
        ["Auto-Translate"] = "自动翻译",
        ["Auto Text Copy Target"] = "自动复制文本目标",
        ["Auto Detect"] = "自动检测",
        ["Auto Set"] = "自动设置",
        ["Available Languages"] = "可用语言",
        ["Background Color"] = "背景颜色",
        ["Background Opacity"] = "背景不透明度",
        ["Bitmap Position (Primary) (0-150%)"] = "位图位置（主字幕，0–150%）",
        ["Bitmap Scale (Primary)"] = "位图缩放（主字幕）",
        ["Bitmap Scale (Secondary)"] = "位图缩放（副字幕）",
        ["Buffer Duration Max (ms)"] = "最大缓冲时长（毫秒）",
        ["Buffer Duration Min (ms)"] = "最小缓冲时长（毫秒）",
        ["Bottom"] = "底部",
        ["Cancel"] = "取消",
        ["Center"] = "居中",
        ["Change Font Size"] = "更改字体大小",
        ["Chapters"] = "章节",
        ["Cheat Sheet"] = "快捷键说明",
        ["Check"] = "检查",
        ["Clear search (Esc)"] = "清除搜索（Esc）",
        ["Close"] = "关闭",
        ["Close the dialog without saving to the config file. The changes are reflected until restart."] = "关闭窗口但不保存到配置文件。更改会在重启前保持有效。",
        ["Code"] = "代码",
        ["Commit: "] = "提交：",
        ["Commit:"] = "提交：",
        ["Common: "] = "通用：",
        ["Configure"] = "配置",
        ["Copy path"] = "复制路径",
        ["Copy to Clipboard"] = "复制到剪贴板",
        ["Copy version to clipboard"] = "复制版本信息到剪贴板",
        ["Copy words on selected"] = "复制所选文字",
        ["Copy the text subtitle to the clipboard when it changes.\nNote that this does not work for bitmap subtitles. In that case, please use OCR to convert them to text."] = "文本字幕变化时自动复制到剪贴板。\n此功能不适用于位图字幕，请先使用 OCR 将其转换为文本。",
        ["Current Color"] = "当前颜色",
        ["Debug"] = "调试",
        ["Default Device"] = "默认设备",
        ["Default Volume (%)"] = "默认音量（%）",
        ["Decoder Threads"] = "解码线程数",
        ["Decrease"] = "减小",
        ["Deinterlace"] = "去隔行",
        ["Delay (ms)"] = "延迟（毫秒）",
        ["Delete"] = "删除",
        ["Description"] = "说明",
        ["Details"] = "详细信息",
        ["Device"] = "设备",
        ["Devices"] = "设备",
        ["Disable"] = "禁用",
        ["Download"] = "下载",
        ["Download Engine"] = "下载引擎",
        ["Download Model"] = "下载模型",
        ["Download subtitles"] = "下载字幕",
        ["Downloaded Languages"] = "已下载语言",
        ["Duration"] = "时长",
        ["Embedded"] = "内嵌",
        ["Enabled"] = "已启用",
        ["Endpoint"] = "服务地址",
        ["Error"] = "错误",
        ["Error Message"] = "错误信息",
        ["Exception Details"] = "异常详情",
        ["Exit App"] = "退出应用",
        ["Export"] = "导出",
        ["Export subtitles as SRT"] = "导出字幕为 SRT",
        ["External"] = "外部",
        ["Extra Arguments"] = "额外参数",
        ["Fallback Source Language (Primary)"] = "备用源语言（主字幕）",
        ["Fallback Source Language (Secondary)"] = "备用源语言（副字幕）",
        ["FFmpeg"] = "FFmpeg",
        ["FileName"] = "文件名",
        ["Filters"] = "滤镜",
        ["Font Color"] = "字体颜色",
        ["Font Color (2nd)"] = "副字幕颜色",
        ["Font Size"] = "字体大小",
        ["Font Size (1st)"] = "主字幕字号",
        ["Font Size (2nd)"] = "副字幕字号",
        ["Font size decrease"] = "减小字号",
        ["Font size increase"] = "增大字号",
        ["Fonts"] = "字体",
        ["Format"] = "格式",
        ["From 1st"] = "来自主字幕",
        ["From 2nd"] = "来自副字幕",
        ["Get Models"] = "获取模型",
        ["Global"] = "全局",
        ["Global Unknown Error"] = "全局未知错误",
        ["Unhandled Exception: "] = "未处理的异常：",
        ["Unknown Error"] = "未知错误",
        ["HDR to SDR Method"] = "HDR 转 SDR 方法",
        ["Idle Timeout (ms)"] = "空闲超时（毫秒）",
        ["Ignore Line Break"] = "忽略换行",
        ["HW Acceleration"] = "硬件加速",
        ["Increase"] = "增加",
        ["Key"] = "按键",
        ["Keyboard"] = "键盘",
        ["Keys"] = "快捷键",
        ["Language"] = "界面语言",
        ["Language settings"] = "界面语言设置",
        ["Left"] = "左侧",
        ["Load"] = "加载",
        ["Loading..."] = "正在加载…",
        ["Log File"] = "日志文件",
        ["Log Level"] = "日志级别",
        ["Log Level (ffmpeg)"] = "FFmpeg 日志级别",
        ["Logging"] = "日志",
        ["Loop Playback"] = "循环播放",
        ["Manual"] = "手动",
        ["Mouse"] = "鼠标",
        ["Mouse Wheel to Volume Up/Down"] = "滚轮调节音量",
        ["Mute / Unmute"] = "静音／取消静音",
        ["Name"] = "名称",
        ["Next Playlist Item"] = "下一个播放列表项目",
        ["No Supported Service"] = "没有受支持的服务",
        ["OCR"] = "文字识别",
        ["OCR Engine (Primary)"] = "OCR 引擎（主字幕）",
        ["OCR Engine (Secondary)"] = "OCR 引擎（副字幕）",
        ["Off"] = "关闭",
        ["On"] = "开启",
        ["Open File"] = "打开文件",
        ["Open Folder"] = "打开文件夹",
        ["Open Settings"] = "打开设置",
        ["Open Timeout (ms)"] = "打开超时（毫秒）",
        ["Order"] = "顺序",
        ["OS"] = "操作系统",
        ["OS Architecture: "] = "操作系统架构：",
        ["OS Architecture:"] = "操作系统架构：",
        ["Paste URL"] = "粘贴网址",
        ["Path"] = "路径",
        ["Play / Pause"] = "播放／暂停",
        ["Player"] = "播放器",
        ["Player UI"] = "播放器界面",
        ["Position Alignment"] = "位置对齐",
        ["Position Alignment (When Dual)"] = "双字幕位置对齐",
        ["Position Offset (%)"] = "位置偏移（%）",
        ["Plugin"] = "插件",
        ["Plugins"] = "插件",
        ["Position / Size"] = "位置／大小",
        ["Primary Color"] = "主色",
        ["Primary Subtitles"] = "主字幕",
        ["Process Architecture: "] = "进程架构：",
        ["Process Architecture:"] = "进程架构：",
        ["Rate"] = "速率",
        ["Record"] = "录制",
        ["Reset"] = "重置",
        ["Reset to default"] = "恢复默认值",
        ["Restart App"] = "重启应用",
        ["Right Click"] = "右键单击",
        ["Save & Close"] = "保存并关闭",
        ["Save &amp; Close"] = "保存并关闭",
        ["Search"] = "搜索",
        ["Search (Ctrl+F)"] = "搜索（Ctrl+F）",
        ["Search Local Paths"] = "搜索本地路径",
        ["Search subtitles..."] = "搜索字幕…",
        ["Secondary Color"] = "辅助色",
        ["Secondary Subtitles"] = "副字幕",
        ["Seek Accurate Fix Margin (ms)"] = "精确跳转修正边距（毫秒）",
        ["SeekBar FadeIn Time (ms)"] = "进度条淡入时间（毫秒）",
        ["SeekBar FadeOut Time (ms)"] = "进度条淡出时间（毫秒）",
        ["SeekBar Height"] = "进度条高度",
        ["SeekBar Thumb Height"] = "进度条滑块高度",
        ["SeekBar Track Height"] = "进度条轨道高度",
        ["Seek"] = "跳转",
        ["Select Model:"] = "选择模型：",
        ["Select OCR Engine"] = "选择 OCR 引擎",
        ["Select specific region"] = "选择特定地区",
        ["Settings"] = "设置",
        ["Show Debug"] = "显示调试信息",
        ["Show Sidebar"] = "显示侧边栏",
        ["Show SeekBar only when MouseOver"] = "仅在鼠标悬停时显示进度条",
        ["Sidebar Left"] = "侧边栏置左",
        ["Sidebar Width"] = "侧边栏宽度",
        ["Single Click to Play/Pause"] = "单击播放／暂停",
        ["Size"] = "大小",
        ["Source Language Region"] = "源语言地区",
        ["Specify idle time in milliseconds to hide seek bar and mouse cursor"] = "设置隐藏进度条和鼠标光标的空闲时间（毫秒）。",
        ["Streams"] = "流",
        ["Subtitles"] = "字幕",
        ["Subtitles (Position/Size)"] = "字幕（位置／大小）",
        ["Subtitles Sidebar"] = "字幕侧边栏",
        ["Subtitles Auto Copy"] = "自动复制字幕",
        ["Subtitles Fonts"] = "字幕字体",
        ["Subtitles Language"] = "字幕语言",
        ["Subtitles Position Up/Down"] = "字幕位置上移／下移",
        ["Subtitles Size Increase/Decrease"] = "字幕字号增大／减小",
        ["Take a Snapshot"] = "截取画面",
        ["Tesseract OCR"] = "Tesseract OCR",
        ["Themes"] = "主题",
        ["Timeout (ms)"] = "超时（毫秒）",
        ["Title"] = "标题",
        ["Toggle Fullscreen"] = "切换全屏",
        ["Toggle Play/Pause"] = "切换播放／暂停",
        ["Top"] = "顶部",
        ["Translate"] = "翻译",
        ["Translated"] = "已翻译",
        ["Translation Engine"] = "翻译引擎",
        ["Translation Language"] = "翻译语言",
        ["Translation Parameters"] = "翻译参数",
        ["Type"] = "类型",
        ["Up"] = "上",
        ["Using libraries"] = "使用的库",
        ["Version: "] = "版本：",
        ["Version:"] = "版本：",
        ["Video"] = "视频",
        ["Video Acceleration"] = "视频加速",
        ["Video Frames Max"] = "视频帧最大数",
        ["Video Processor"] = "视频处理器",
        ["Video Streams"] = "视频流",
        ["Visibility"] = "可见性",
        ["Volume Up / Down"] = "音量增大／减小",
        ["Wheel"] = "滚轮",
        ["Wheel Click"] = "单击滚轮",
        ["Width (1-100%)"] = "宽度（1–100%）",
        ["Word Action"] = "划词操作",
        ["Word Actions"] = "划词操作",
        ["Word Lookup"] = "查词",
        ["Word Click Action:"] = "点击单词时的操作：",
        ["Accurate"] = "精确",
        ["Add Clipboard"] = "添加剪贴板操作",
        ["Add ClipboardAll"] = "添加复制全部操作",
        ["Add Search"] = "添加搜索操作",
        ["Adjust the delay so that this subtitle is currently playing."] = "调整延迟，使当前字幕与播放位置同步。",
        ["Always Seek Accurate"] = "始终精确跳转",
        ["API Key"] = "API 密钥",
        ["API Key (optional)"] = "API 密钥（可选）",
        ["App"] = "应用",
        ["AudioContextSize"] = "音频上下文大小",
        ["Available Options"] = "可用选项",
        ["Background Opacity (2nd)"] = "副字幕背景不透明度",
        ["Bitmap Scale Offset (%)"] = "位图缩放偏移（%）",
        ["Clone App"] = "克隆应用",
        ["Clone Row"] = "克隆行",
        ["Copy Debug Command"] = "复制调试命令",
        ["Copy Help Command"] = "复制帮助命令",
        ["Delay Offset1 (ms)"] = "延迟偏移 1（毫秒）",
        ["Delay Offset1 (ms) "] = "延迟偏移 1（毫秒）",
        ["Delay Offset2 (ms)"] = "延迟偏移 2（毫秒）",
        ["Delay Primary (ms)"] = "主字幕延迟（毫秒）",
        ["Delay Secondary (ms)"] = "副字幕延迟（毫秒）",
        ["Delete Row"] = "删除行",
        ["Distance Offset"] = "间距偏移",
        ["Do Action"] = "执行操作",
        ["Do last search on selected"] = "对所选文字执行上次搜索",
        ["Double Click to FullScreen"] = "双击切换全屏",
        ["Double Rate"] = "双倍速度",
        ["Down"] = "下",
        ["DownloadsCnt"] = "下载次数",
        ["Export UTF8 with BOM"] = "导出带 BOM 的 UTF-8",
        ["FFmpeg Load Profile"] = "FFmpeg 加载配置",
        ["Fire an action"] = "触发操作",
        ["Fire an action with the menu open"] = "打开菜单时触发操作",
        ["Fix Overflow Bottom Margin (0-100%)"] = "修正底部溢出边距（0–100%）",
        ["Flip"] = "翻转",
        ["Font Size Offset"] = "字号偏移",
        ["Fonts (2nd)"] = "副字幕字体",
        ["Fonts..."] = "字体…",
        ["Hello API"] = "测试 API",
        ["Horizontal Flip"] = "水平翻转",
        ["Input Enter"] = "输入后按回车",
        ["Keys > Offset"] = "快捷键 > 偏移",
        ["Last Word Action (Search)"] = "上次划词操作（搜索）",
        ["Launch App"] = "启动应用",
        ["Left Click"] = "左键单击",
        ["Left Click with Ctrl"] = "Ctrl + 左键单击",
        ["Left DoubleClick"] = "左键双击",
        ["Left Drag"] = "左键拖动",
        ["LLM Parameters"] = "大语言模型参数",
        ["LLPlayer cannot exist without the following libraries!"] = "LLPlayer 的运行离不开以下库！",
        ["Loaded Option: "] = "已加载选项：",
        ["Loaded Option:"] = "已加载选项：",
        ["Log Cached Lines"] = "日志缓存行数",
        ["Mask subtitles after current (to prevent spoiler)"] = "隐藏当前字幕后的内容（避免剧透）",
        ["Max Latency (ms)"] = "最大延迟（毫秒）",
        ["Max Translate Concurrency"] = "最大并发翻译数",
        ["Max Translate Count (Backward)"] = "向后最大翻译条数",
        ["Max Translate Count (Forward)"] = "向前最大翻译条数",
        ["Max Volume (1-1000%)"] = "最大音量（1–1000%）",
        ["MaxTokensPerSegment"] = "每段最大令牌数",
        ["Media title on Seekbar"] = "进度条显示媒体标题",
        ["Microsoft OCR"] = "微软 OCR",
        ["Modifier:"] = "修饰键：",
        ["Move Down (right selected libraries)"] = "下移（右侧已选库）",
        ["Move Left"] = "左移",
        ["Move Right"] = "右移",
        ["Move Up (right selected libraries)"] = "上移（右侧已选库）",
        ["Move Video Viewport"] = "移动视频视口",
        ["NoContext"] = "不使用上下文",
        ["NoSpeechThreshold"] = "无语音阈值",
        ["Offset"] = "偏移",
        ["On Context Menu"] = "在右键菜单中",
        ["On Text Subtitles (including sidebar)"] = "在文本字幕中（含侧边栏）",
        ["On Video"] = "在视频上",
        ["Open Context Menu"] = "打开右键菜单",
        ["Open file dialog"] = "打开文件选择窗口",
        ["Open next"] = "打开下一个",
        ["Open path"] = "打开路径",
        ["Open prev"] = "打开上一个",
        ["PDIC Executable Path"] = "PDIC 可执行文件路径",
        ["Phrase Lookup"] = "短语查询",
        ["Player Timeout (For Live Stream)"] = "播放器超时（直播）",
        ["Please configure the settings in advance in the Translate tab."] = "请先在“翻译”选项卡中完成设置。",
        ["Previous Playlist Item"] = "上一个播放列表项目",
        ["Primary/Secondary Distance"] = "主／副字幕间距",
        ["Profile Mismatch"] = "配置不匹配",
        ["Query:"] = "查询：",
        ["Read Live Timeout (ms)"] = "读取直播超时（毫秒）",
        ["Read Timeout (ms)"] = "读取超时（毫秒）",
        ["Region (optional)"] = "地区（可选）",
        ["Reset ..."] = "重置…",
        ["Reset all"] = "全部重置",
        ["Reset from 1st"] = "从主字幕重置",
        ["Reset..."] = "重置…",
        ["Reverse Playback"] = "反向播放",
        ["Same as Primary"] = "与主字幕相同",
        ["SDR Display Nits"] = "SDR 显示亮度（尼特）",
        ["Seek Offset1 (ms)"] = "跳转偏移 1（毫秒）",
        ["Seek Offset2 (ms)"] = "跳转偏移 2（毫秒）",
        ["Seek Offset3 (ms)"] = "跳转偏移 3（毫秒）",
        ["Seek Offset4 (ms)"] = "跳转偏移 4（毫秒）",
        ["Seek Timeout (ms)"] = "跳转超时（毫秒）",
        ["Seek to current subtitle if available"] = "如可用则跳转到当前字幕",
        ["Seek to the next subtitle or forwards"] = "跳转到下一条字幕或向前跳转",
        ["Seek to the previous subtitle or backwards"] = "跳转到上一条字幕或向后跳转",
        ["Selected Languages (by priority)"] = "按优先级选择的语言",
        ["Selected Options (Priority to the above)"] = "已选选项（优先于上项）",
        ["Sentence Lookup"] = "句子查询",
        ["Separator Max Width"] = "分隔符最大宽度",
        ["Set Default"] = "设为默认",
        ["Several actions on a current video"] = "对当前视频执行的操作",
        ["Shortcut"] = "快捷方式",
        ["Show Original Text when translating enabled"] = "启用翻译时显示原文",
        ["Speed Offset1"] = "速度偏移 1",
        ["Speed Offset2"] = "速度偏移 2",
        ["SplitOnWord"] = "按词拆分",
        ["Stroke Color"] = "描边颜色",
        ["Stroke Thickness"] = "描边粗细",
        ["Subs Downloader"] = "字幕下载器",
        ["Subs Exporter"] = "字幕导出器",
        ["Subtitle"] = "字幕",
        ["Subtitle Padding"] = "字幕内边距",
        ["Subtitles > ASR"] = "字幕 > 语音识别",
        ["Subtitles > OCR"] = "字幕 > 文字识别",
        ["Subtitles > Position / Size"] = "字幕 > 位置／大小",
        ["Subtitles > Translate"] = "字幕 > 翻译",
        ["Subtitles > Word Action"] = "字幕 > 划词操作",
        ["Subtitles Language Auto-Open (Priority to the above)"] = "自动打开字幕语言（优先于上项）",
        ["Super Resolution"] = "超分辨率",
        ["Swap sidebar position"] = "交换侧边栏位置",
        ["Timeout Health (ms)"] = "健康检查超时（毫秒）",
        ["Toggle hiding after current subtitles"] = "切换隐藏当前字幕后的内容",
        ["Toggle Primary / Secondary"] = "切换主／副字幕",
        ["Toggle Sub Sidebar"] = "切换字幕侧边栏",
        ["Toggle to show original text when translating enabled"] = "切换翻译时显示原文",
        ["Translation Chat Config (for LLM API such as Ollama, OpenAI, etc...)"] = "翻译聊天配置（适用于 Ollama、OpenAI 等大语言模型 API）",
        ["Use Filters"] = "使用滤镜",
        ["Use Separate 2nd Fonts"] = "副字幕使用单独字体",
        ["V.Sync"] = "垂直同步",
        ["Vertical Flip"] = "垂直翻转",
        ["Vertical Position (-25%-150%)"] = "垂直位置（-25%–150%）",
        ["Vertical Resolution Max"] = "最大垂直分辨率",
        ["Volume 50%"] = "音量 50%",
        ["Volume Offset"] = "音量偏移",
        ["Wheel to Volume Up/Down"] = "滚轮调节音量",
        ["When Dual"] = "双字幕时",
        ["Whisper Common"] = "Whisper 通用",
        ["whisper.cpp config"] = "whisper.cpp 配置",
        ["whisper.cpp hardware options (Required to restart to apply changes)"] = "whisper.cpp 硬件选项（需要重启后生效）",
        ["With UTF8 BOM"] = "带 UTF-8 BOM",
        ["Word Actions (Search/Copy)"] = "划词操作（搜索／复制）",
        ["Word Context Menu"] = "单词右键菜单",
        ["Word Special Actions for Japanese"] = "日语单词特殊操作",
        ["Word Translation Engine"] = "单词翻译引擎",
        ["Year"] = "年份",
        ["Yes"] = "是",
        ["Zero Latency"] = "零延迟",
        ["Zoom In"] = "放大",
        ["Zoom In / Out"] = "放大／缩小",
        ["Zoom out"] = "缩小",
        ["Zoom Reset"] = "重置缩放",
        ["Zoom Unit"] = "缩放单位",
        ["Zoom Unit (%)"] = "缩放单位（%）",
        ["Open Next File"] = "打开下一个文件",
        ["Open Previous File"] = "打开上一个文件",
        ["Open Folder or URL of the currently opened file"] = "打开当前文件所在文件夹或网址",
        ["Subtitles Position Up"] = "字幕位置上移",
        ["Subtitles Position Down"] = "字幕位置下移",
        ["Subtitles Size Increase"] = "增大字幕字号",
        ["Subtitles Size Decrease"] = "减小字幕字号",
        ["Primary Subtitles Size Increase"] = "增大主字幕字号",
        ["Primary Subtitles Size Decrease"] = "减小主字幕字号",
        ["Secondary Subtitles Size Increase"] = "增大副字幕字号",
        ["Secondary Subtitles Size Decrease"] = "减小副字幕字号",
        ["Primary/Secondary Subtitles Distance Increase"] = "增大主／副字幕间距",
        ["Primary/Secondary Subtitles Distance Decrease"] = "减小主／副字幕间距",
        ["Copy All Subtiltes Text"] = "复制全部字幕文本",
        ["Copy Primary Subtiltes Text"] = "复制主字幕文本",
        ["Copy Secondary Subtiltes Text"] = "复制副字幕文本",
        ["Toggle Auto Subtitles Text Copy"] = "切换自动复制字幕文本",
        ["Toggle Primary / Secondary in Subtitles Sidebar"] = "在字幕侧边栏切换主／副字幕",
        ["Toggle to show original text in Subtitles Sidebar"] = "在字幕侧边栏切换显示原文",
        ["Activate Subtitles Search in Sidebar"] = "激活侧边栏字幕搜索",
        ["Toggle Subitltes Sidebar"] = "切换字幕侧边栏",
        ["Toggle Debug Overlay"] = "切换调试叠加层",
        ["Toggle Always On Top"] = "切换窗口置顶",
        ["Open Settings Window"] = "打开设置窗口",
        ["Open Subtitles Downloader Window"] = "打开字幕下载窗口",
        ["Open Subtitles Exporter Window"] = "打开字幕导出窗口",
        ["Open Cheat Sheet Window"] = "打开快捷键说明窗口",
        ["Launch New Application"] = "启动新应用实例",
        ["Launch Clone Application"] = "启动克隆应用实例",
        ["Restart Application"] = "重启应用",
        ["Exit Application"] = "退出应用",
        ["[Is English Model]"] = "[英语专用模型]",
        ["Manual Engine Path"] = "手动指定引擎路径",
        ["Manual Model Directory"] = "手动指定模型目录",
        ["  Search Local Paths"] = "  搜索本地路径",
        ["0 for Auto"] = "0 表示自动",
        ["1st Visible"] = "第一个可见",
        ["2nd Visible"] = "第二个可见",
        ["An upper bound for the number of tokens that can be generated for a completion, including visible output tokens and reasoning tokens."] = "一次生成可使用的最大令牌数，包含可见输出令牌和推理令牌。",
        ["Automatically detects language from text subtitle. Accurate language identification allows for the translation function. Memory usage will increase slightly."] = "自动从文本字幕识别语言。准确的语言识别可启用翻译功能，但会略微增加内存占用。",
        ["Default: Whisper\\Faster-Whisper-XXL\\faster-whisper-xxl.exe"] = "默认：Whisper\\Faster-Whisper-XXL\\faster-whisper-xxl.exe",
        ["Default: whispermodels\\"] = "默认：whispermodels\\",
        ["If enabled, automatic subtitles such as those on YouTube are automatically opened. If disabled, they will be added but not auto-open."] = "启用后会自动打开 YouTube 等来源的自动字幕；禁用后字幕会被添加，但不会自动打开。",
        ["If enabled, internal bitmap subtitles can be displayed in the sidebar. and internal bitmap subtitles are always displayed during seek, However, memory usage will increase."] = "启用后可在侧边栏显示内嵌位图字幕，跳转时也会始终显示；但会增加内存占用。",
        ["If enabled, subtitle files in the same path as the media file are automatically opened. The following subtitle files will be opened."] = "启用后会自动打开与媒体文件同路径的字幕文件。以下字幕文件将被打开。",
        ["If this is switched on, the seek bar is only displayed on mouse-over; if it is switched off, it is displayed when the cursor is moved."] = "启用后仅在鼠标悬停时显示进度条；关闭后移动鼠标即可显示。",
        ["If you run it again when it is already enabled, subtitles will be regenerated from the playback position."] = "已启用时再次运行，会从当前播放位置重新生成字幕。",
        ["IsKeyUp"] = "按键抬起",
        ["Larger models increase load and accuracy. Please choose the appropriate model for your hardware. Models ending in .en are only available in English."] = "更大的模型会提高负载和准确率，请按硬件能力选择。以 .en 结尾的模型仅支持英语。",
        ["Parameter for how many MB of audio to accumulate and pass to ASR. Increasing it increase memory usage and may cause natural breaks in sentences."] = "传递给语音识别前累积的音频大小（MB）。增大此值会提高内存占用，并可能让句子在自然位置断开。",
        ["Parameter for how many seconds to wait to accumulate and pass to ASR. It is the real time, not the audio time. This parameter is ORed with Audio Chunk Size, and is used for when the size of the video is small, such as in the case of live video."] = "传递给语音识别前等待累积音频的秒数。这是现实时间而非音频时间；它与音频块大小取任一条件满足，适用于直播等音频数据较小的场景。",
        ["Prompt to send to chat. Certain strings will be replaced by the following."] = "发送给聊天模型的提示词。其中部分字符串会替换为以下内容。",
        ["Separator to be displayed when dual subtitles are used. If you do not want to display it, set it to 0."] = "双字幕时显示的分隔符。不希望显示时请设为 0。",
        ["Set your native language. It will be translated into that language. If it is in the same language as the video, it will not be translated. Some engines do not support certain languages."] = "设置你的母语，字幕会翻译成该语言。若它与视频语言相同，则不会翻译；部分引擎不支持某些语言。",
        ["Sets the maximum width of the subtitle; if set to 0, no limit is set."] = "设置字幕最大宽度；设为 0 表示不限制。",
        ["Specifies the GPU device number to be used, index starting from 0. The first device is used by default."] = "指定要使用的 GPU 设备编号，索引从 0 开始；默认使用第一块设备。",
        ["Specify additional folder paths to open subtitles, separated by semicolons. If left blank, subtitles will be opened only from the current directory. Folder case is ignored. Both relative and absolute paths can be specified."] = "指定额外的字幕搜索文件夹，以分号分隔。留空时仅从当前目录打开字幕；文件夹路径不区分大小写，支持相对和绝对路径。",
        ["Specify the full path to faster-whisper-xxl.exe or whisper-faster.exe."] = "指定 faster-whisper-xxl.exe 或 whisper-faster.exe 的完整路径。",
        ["Specify the number of threads. This will increase the load, but will speed up subtitle generation. Do not specify more than CPU threads."] = "指定线程数。线程数增加会提高负载，但会加快字幕生成；请勿超过 CPU 线程数。",
        ["The model is automatically downloaded at the first run. Note that if the size is large, it will take longer. Models starting with distil- and ending in .en are only available in English."] = "模型会在首次运行时自动下载。模型较大时下载耗时更长；以 distil- 开头且以 .en 结尾的模型仅支持英语。",
        ["The number of subtitles to be sent together. Increasing the number increases the accuracy of the translation, but also the load and tokens."] = "一次共同发送的字幕条数。增加条数可提高翻译准确度，但也会增加负载和令牌消耗。",
        ["Translated into English using Whisper's built-in translation engine. Only English is supported. If you want to translate into another language, you can use the LLPlayer's translation engine."] = "使用 Whisper 内置翻译引擎翻译为英语，仅支持英语。如需翻译为其他语言，请使用 LLPlayer 的翻译引擎。",
        ["True for English-only models, in which case the Audio Language setting above is ignored."] = "英语专用模型请设为“是”，此时会忽略上方的音频语言设置。",
        ["WARN: ASR/OCR is running, so not all are saved."] = "警告：语音识别／文字识别正在运行，因此不会保存全部内容。",
        ["Whether to include regional information in the {target_lang} of the prompt as follows."] = "是否在提示词的 {target_lang} 中包含地区信息。",
        ["X1 Click"] = "X1 键单击",
        ["X2 Click"] = "X2 键单击",
        ["You can change the priority of the whisper process. If playback is affected, you can lower the priority."] = "可以调整 Whisper 进程优先级。若影响播放，请降低优先级。",
        ["You can copy debugging commands to the clipboard, which can then be executed in PowerShell by pasting."] = "可将调试命令复制到剪贴板，再粘贴到 PowerShell 中执行。",
        ["You can limit the number of characters per subtitle. If the default subtitle is too long, you can adjust it by specifying the number of characters."] = "可限制每条字幕的字符数。默认字幕过长时，可通过指定字符数调整。",
        ["You may be able to customize the output. For example, you can control whether the output is in Simplified or Traditional Chinese."] = "可自定义输出，例如控制输出为简体中文或繁体中文。",
        ["English"] = "英语",
        ["Simplified Chinese"] = "简体中文",
        ["Restart the application after saving to apply the interface language."] = "保存后请重启应用以应用界面语言。",
        ["Choose the language used by menus, settings, and dialog text."] = "选择菜单、设置和对话框使用的界面语言。",
        ["Required to restart to apply changes"] = "需要重启后才能应用更改",
        ["Writes to the config file and closes the dialog."] = "写入配置文件并关闭此窗口。",
    };

    public static UiLanguage StartupLanguage { get; } = ReadStartupLanguage();

    public static void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        _initialized = true;
        _language = StartupLanguage;
        EventManager.RegisterClassHandler(typeof(FrameworkElement), FrameworkElement.LoadedEvent, new RoutedEventHandler(OnElementLoaded));
    }

    public static CultureInfo GetCulture() => _language == UiLanguage.ChineseSimplified
        ? CultureInfo.GetCultureInfo("zh-CN")
        : CultureInfo.GetCultureInfo("en-US");

    private static UiLanguage ReadStartupLanguage()
    {
        try
        {
            if (!File.Exists(App.AppConfigPath))
            {
                return UiLanguage.English;
            }

            using JsonDocument document = JsonDocument.Parse(File.ReadAllText(App.AppConfigPath));
            if (document.RootElement.TryGetProperty(nameof(AppConfig.UiLanguage), out JsonElement value) &&
                Enum.TryParse(value.GetString(), ignoreCase: true, out UiLanguage language))
            {
                return language;
            }
        }
        catch (Exception)
        {
            // 配置读取失败时沿用英文，避免影响应用启动。
        }

        return UiLanguage.English;
    }

    private static void OnElementLoaded(object sender, RoutedEventArgs e)
    {
        if (_language != UiLanguage.ChineseSimplified || sender is not FrameworkElement element)
        {
            return;
        }

        TranslateElement(element);
    }

    /// <summary>
    /// 显式翻译一棵已创建的控件树，用于动态切换的设置页面。
    /// </summary>
    public static void ApplyTo(DependencyObject root)
    {
        if (_language != UiLanguage.ChineseSimplified)
        {
            return;
        }

        TranslateTree(root);
    }

    private static void TranslateTree(DependencyObject current)
    {
        if (current is FrameworkElement element)
        {
            TranslateElement(element);
        }

        int childCount = VisualTreeHelper.GetChildrenCount(current);
        for (int index = 0; index < childCount; index++)
        {
            TranslateTree(VisualTreeHelper.GetChild(current, index));
        }
    }

    private static void TranslateElement(FrameworkElement element)
    {
        if (element is Window window && !BindingOperations.IsDataBound(window, Window.TitleProperty))
        {
            window.Title = Translate(window.Title);
        }

        if (element is TextBlock textBlock && !BindingOperations.IsDataBound(textBlock, TextBlock.TextProperty))
        {
            // TextBlock 中含图标时，文字会被解析为 Run，直接设置 Text 会丢失图标。
            if (textBlock.Inlines.Count == 0)
            {
                textBlock.Text = Translate(textBlock.Text);
            }
            else
            {
                var runs = new List<Run>();
                foreach (Inline inline in textBlock.Inlines)
                {
                    if (inline is Run run)
                    {
                        runs.Add(run);
                    }
                }

                foreach (Run run in runs)
                {
                    run.Text = Translate(run.Text.Trim());
                }
            }
        }

        if (element is ContentControl contentControl && contentControl.Content is string content &&
            !BindingOperations.IsDataBound(contentControl, ContentControl.ContentProperty))
        {
            contentControl.Content = Translate(content);
        }

        if (element is HeaderedContentControl headeredContent && headeredContent.Header is string header &&
            !BindingOperations.IsDataBound(headeredContent, HeaderedContentControl.HeaderProperty))
        {
            headeredContent.Header = Translate(header);
        }

        if (element is HeaderedItemsControl headeredItems && headeredItems.Header is string itemsHeader &&
            !BindingOperations.IsDataBound(headeredItems, HeaderedItemsControl.HeaderProperty))
        {
            headeredItems.Header = Translate(itemsHeader);
        }

        object toolTip = ToolTipService.GetToolTip(element);
        if (toolTip is string text)
        {
            ToolTipService.SetToolTip(element, Translate(text));
        }
        else if (toolTip is ToolTip { Content: string toolTipContent } materialToolTip)
        {
            // Material Design 会将附加的 ToolTip 字符串包装成 ToolTip 对象。
            materialToolTip.Content = Translate(toolTipContent);
        }

        if (element is ListView { View: GridView gridView })
        {
            foreach (GridViewColumn column in gridView.Columns)
            {
                if (column.Header is string columnHeader)
                {
                    column.Header = Translate(columnHeader);
                }
            }
        }
    }

    /// <summary>
    /// 为动态生成的界面文字提供与 XAML 相同的翻译入口。
    /// </summary>
    public static string TranslateText(string text) => _language == UiLanguage.ChineseSimplified ? Translate(text) : text;

    private static string Translate(string text) => Chinese.TryGetValue(text, out string? translation) ? translation : text;
}
