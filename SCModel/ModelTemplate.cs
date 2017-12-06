  
using System;
namespace SCModel
{    

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Course_Chapter
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		///课程编号 
		/// </summary>
		public int? CourseID { get; set; }
		/// <summary>
		///章节名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///父节点 
		/// </summary>
		public int? Pid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Code { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Sort { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Couse_Resource
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? CouseID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ChapterID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? ResourcesID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsVideo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsAllowDown { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string VidoeImag { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Acti_DepartHisMember
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///部门Id 
		/// </summary>
		public int? DepartId { get; set; }
		/// <summary>
		///成员 
		/// </summary>
		public string MemberNo { get; set; }
		/// <summary>
		///个人介绍 
		/// </summary>
		public string Introduction { get; set; }
		/// <summary>
		///加入时间 
		/// </summary>
		public DateTime? JoinTime { get; set; }
		/// <summary>
		///退出时间 
		/// </summary>
		public DateTime? OutTime { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Com_Favorites
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///类型  0社团；1部门；2宿舍 
		/// </summary>
		public Byte? Type { get; set; }
		/// <summary>
		///关联Id 
		/// </summary>
		public int? RelationId { get; set; }
		/// <summary>
		///名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///链接 
		/// </summary>
		public string Href { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Acti_DepartInfo
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///部门名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///父Id 
		/// </summary>
		public int? ParentId { get; set; }
		/// <summary>
		///部长 
		/// </summary>
		public string LeaderNo { get; set; }
		/// <summary>
		///副部长 
		/// </summary>
		public string SecondLeaderNo { get; set; }
		/// <summary>
		///部门介绍 
		/// </summary>
		public string Introduce { get; set; }
		/// <summary>
		///部门Logo 
		/// </summary>
		public string PicURL { get; set; }
		/// <summary>
		///部门背景图 
		/// </summary>
		public string BackPicUrl { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Acti_DepartMember
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///部门Id 
		/// </summary>
		public int? DepartId { get; set; }
		/// <summary>
		///成员 
		/// </summary>
		public string MemberNo { get; set; }
		/// <summary>
		///个人介绍 
		/// </summary>
		public string Introduction { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class ClassCourse
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		///班级编号 
		/// </summary>
		public int? ClassID { get; set; }
		/// <summary>
		///课程编号 
		/// </summary>
		public int? CourseID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Acti_Project
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///活动Id 
		/// </summary>
		public int? ActivityId { get; set; }
		/// <summary>
		///项目名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///参加成员 
		/// </summary>
		public string JoinMembers { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Com_Message
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Contents { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? Type { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Receiver { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ReceiverName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Href { get; set; }
		/// <summary>
		///是否已读 
		/// </summary>
		public Byte? Status { get; set; }
		/// <summary>
		///是否发送 
		/// </summary>
		public Byte? IsSend { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ReceiverEmail { get; set; }
		/// <summary>
		///是否是定时发送 
		/// </summary>
		public Byte? Timing { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string FilePath { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class ModelCatogory
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Status { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? SortNum { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Acti_RecruitApply
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///活动Id 
		/// </summary>
		public int? ActivityId { get; set; }
		/// <summary>
		///申请类型 1入部申请，2退部申请 
		/// </summary>
		public Byte? Type { get; set; }
		/// <summary>
		///说明 
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		///审核人 
		/// </summary>
		public string ExamUserNo { get; set; }
		/// <summary>
		///审核意见 
		/// </summary>
		public string ExamSuggest { get; set; }
		/// <summary>
		///审核状态 1待审核（默认），2审核通过，3审核拒绝 
		/// </summary>
		public Byte? ExamStatus { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Acti_WinInfo
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///活动Id 
		/// </summary>
		public int? ActivityId { get; set; }
		/// <summary>
		///奖项名称 
		/// </summary>
		public string AwardName { get; set; }
		/// <summary>
		///奖项等级 
		/// </summary>
		public string AwardGrade { get; set; }
		/// <summary>
		///获奖成员 
		/// </summary>
		public string MemberNo { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class ModelManage
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ModelName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? ModelType { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string OpenType { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ModelCss { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string iconCss { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string LinkUrl { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Pid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? OrderNum { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Boolean? IsMenu { get; set; }
		/// <summary>
		///1.菜单2.按钮 
		/// </summary>
		public int? MenuType { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Asso_Activity
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///社团Id 
		/// </summary>
		public int? AssoId { get; set; }
		/// <summary>
		///活动名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///开始时间 
		/// </summary>
		public DateTime? StartTime { get; set; }
		/// <summary>
		///结束时间 
		/// </summary>
		public DateTime? EndTime { get; set; }
		/// <summary>
		///活动地址 
		/// </summary>
		public string Address { get; set; }
		/// <summary>
		///活动内容 
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		///审核人 
		/// </summary>
		public string ExamUserNo { get; set; }
		/// <summary>
		///审核意见 
		/// </summary>
		public string ExamSuggest { get; set; }
		/// <summary>
		///审核状态  1待审核（默认），2审核通过，3审核拒绝 
		/// </summary>
		public Byte? ExamStatus { get; set; }
		/// <summary>
		///参加成员 
		/// </summary>
		public string JoinMembers { get; set; }
		/// <summary>
		///附件 
		/// </summary>
		public string AttachUrl { get; set; }
		/// <summary>
		///活动图片 
		/// </summary>
		public string ActivityImg { get; set; }
		/// <summary>
		///浏览次数 
		/// </summary>
		public int? BrowsingTimes { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class UserSkin
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string UserID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string SkinImage { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Course_Manage
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		///课程名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///课程图片 
		/// </summary>
		public string ImageUrl { get; set; }
		/// <summary>
		///1:选修课2：必修课 
		/// </summary>
		public Byte? CourceType { get; set; }
		/// <summary>
		///课程状态：0申请1审核通过2审核失败3报名开始4报名结束 
		/// </summary>
		public int? Status { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? StuMaxCount { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CourseIntro { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? StudyTerm { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndTime { get; set; }
		/// <summary>
		///课程评价标准 
		/// </summary>
		public string EvalueStandard { get; set; }
		/// <summary>
		///硬件要求 
		/// </summary>
		public string CourseHardware { get; set; }
		/// <summary>
		///上课场地 
		/// </summary>
		public string StudyPlace { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string GradeID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string WeekName { get; set; }
		/// <summary>
		///选课人数 
		/// </summary>
		public int? ChoiseNum { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CheckMes { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreatUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public int? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class User_Model_Rel
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? ModelID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string UserIDCard { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Asso_Apply
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///社团Id 
		/// </summary>
		public int? AssoId { get; set; }
		/// <summary>
		///申请人 
		/// </summary>
		public string ApplyUserNo { get; set; }
		/// <summary>
		///1入团申请，2退团申请 
		/// </summary>
		public Byte? ApplyType { get; set; }
		/// <summary>
		///申请介绍 
		/// </summary>
		public string ApplyIntroduce { get; set; }
		/// <summary>
		///申请时间 
		/// </summary>
		public DateTime? ApplyTime { get; set; }
		/// <summary>
		///审核人 
		/// </summary>
		public string ExamUserNo { get; set; }
		/// <summary>
		///1待审核（默认），2审核通过，3审核拒绝 
		/// </summary>
		public Byte? ExamStatus { get; set; }
		/// <summary>
		///审核意见 
		/// </summary>
		public string ExamSuggest { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class MyResource
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		///网盘ID 
		/// </summary>
		public int? PID { get; set; }
		/// <summary>
		///文件名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///是否文件夹 
		/// </summary>
		public int? IsFolder { get; set; }
		/// <summary>
		///文件地址 
		/// </summary>
		public string FileUrl { get; set; }
		/// <summary>
		///文件大小 
		/// </summary>
		public int? FileSize { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string FileIcon { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string code { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string postfix { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string FileIconBig { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class ResourceType
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Postfixs { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EidtTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? EditUser { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Asso_EnteredSet
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///报名是否允许多选   0不允许（默认）；1允许
 
		/// </summary>
		public Byte? IsOnly { get; set; }
		/// <summary>
		///开始报名时间 
		/// </summary>
		public DateTime? StartTime { get; set; }
		/// <summary>
		///结束报名时间 
		/// </summary>
		public DateTime? EndTime { get; set; }
		/// <summary>
		///备注 
		/// </summary>
		public string Remark { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class ResourcesInfo
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string FileUrl { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Status { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? DownCount { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CatagoryID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? ChapterID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? ClickCount { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? EvalueCount { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? IsOpen { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CheckMessage { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? FileSize { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EidtTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string FileIcon { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string postfix { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string FileGroup { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? EvalueResult { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsVideo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string FileIconBig { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Asso_HisLeader
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///社团Id 
		/// </summary>
		public int? AssoId { get; set; }
		/// <summary>
		///类型   0社团长；1副社团长 
		/// </summary>
		public Byte? Type { get; set; }
		/// <summary>
		///社团长/副社团长 
		/// </summary>
		public string OSLeaderNo { get; set; }
		/// <summary>
		///上任时间 
		/// </summary>
		public DateTime? OfficeTime { get; set; }
		/// <summary>
		///卸任时间 
		/// </summary>
		public DateTime? OutgoingTime { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Dorm_Room
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///宿舍楼Id 
		/// </summary>
		public int? BuildId { get; set; }
		/// <summary>
		///宿舍名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///楼层号 
		/// </summary>
		public Byte? FloorNo { get; set; }
		/// <summary>
		///床位数 
		/// </summary>
		public Byte? Beds { get; set; }
		/// <summary>
		///宿舍长 
		/// </summary>
		public string ManagerNo { get; set; }
		/// <summary>
		///简介 
		/// </summary>
		public string Introduce { get; set; }
		/// <summary>
		///宿舍Logo 
		/// </summary>
		public string PicURL { get; set; }
		/// <summary>
		///宿舍背景图 
		/// </summary>
		public string BackPicUrl { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Asso_HisMember
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///社团Id 
		/// </summary>
		public int? AssoId { get; set; }
		/// <summary>
		///成员 
		/// </summary>
		public string MemberNo { get; set; }
		/// <summary>
		///个人介绍 
		/// </summary>
		public string Introduction { get; set; }
		/// <summary>
		///加入时间 
		/// </summary>
		public DateTime? JoinTime { get; set; }
		/// <summary>
		///退出时间 
		/// </summary>
		public DateTime? OutTime { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Asso_Info
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///社团名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///社团口号 
		/// </summary>
		public string Slogan { get; set; }
		/// <summary>
		///社团介绍 
		/// </summary>
		public string Introduce { get; set; }
		/// <summary>
		///社团长 
		/// </summary>
		public string LeaderNo { get; set; }
		/// <summary>
		///副团长 
		/// </summary>
		public string SecondLeaderNo { get; set; }
		/// <summary>
		///社团分类 
		/// </summary>
		public int? AssoType { get; set; }
		/// <summary>
		///加入类型 0申请加入（默认）；1公开 
		/// </summary>
		public Byte? ApplyType { get; set; }
		/// <summary>
		///社团Logo 
		/// </summary>
		public string PicURL { get; set; }
		/// <summary>
		///社团背景图 
		/// </summary>
		public string BackPicUrl { get; set; }
		/// <summary>
		///人数限制 
		/// </summary>
		public int? PersonLimit { get; set; }
		/// <summary>
		///性别限制 
		/// </summary>
		public Byte? SexLimit { get; set; }
		/// <summary>
		///年级限制 
		/// </summary>
		public string GradeLimit { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Couse_Selstuinfo
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? CourseID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string StuNo { get; set; }
		/// <summary>
		///第一志愿：1，第二志愿：2，第三志愿：3 
		/// </summary>
		public int? VoluntRank { get; set; }
		/// <summary>
		///0：申请1通过2未通过 
		/// </summary>
		public short? Status { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class ClickDetail
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? ResourcesID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ClickTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? ClickNum { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///1:点击2评价3下载 
		/// </summary>
		public Byte? ClickType { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Schedule
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? StartDate { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EndDate { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? AllDay { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? isEndTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Asso_Member
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///社团Id 
		/// </summary>
		public int? AssoId { get; set; }
		/// <summary>
		///成员 
		/// </summary>
		public string MemberNo { get; set; }
		/// <summary>
		///个人介绍 
		/// </summary>
		public string Introduction { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Asso_Type
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///类型名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Book_Subject
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? VersionID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? GradeID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Com_ActivityDoc
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///类型 0资讯；1社团活动；2部门活动；3宿舍活动 
		/// </summary>
		public Byte? Type { get; set; }
		/// <summary>
		///关联Id 
		/// </summary>
		public int? RelationId { get; set; }
		/// <summary>
		///资料名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///资料url 
		/// </summary>
		public string DataUrl { get; set; }
		/// <summary>
		///扩展名 
		/// </summary>
		public string DataType { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Book_Version
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Com_Album
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///类型  0社团；1部门；2宿舍 
		/// </summary>
		public Byte? Type { get; set; }
		/// <summary>
		///关联Id 
		/// </summary>
		public int? RelationId { get; set; }
		/// <summary>
		///相册名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///相册描述 
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		///点击量 
		/// </summary>
		public int? ClickCount { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Book_Grade
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Course_Attendance
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		///课程ID 
		/// </summary>
		public int? CourseID { get; set; }
		/// <summary>
		///考勤状态 1-正常 2-迟到 3-早退 4-缺课 
		/// </summary>
		public int? AttStatus { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string StuNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string TeaNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Com_AlbumPic
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///相册Id 
		/// </summary>
		public int? AlbumId { get; set; }
		/// <summary>
		///图片url 
		/// </summary>
		public string PicUrl { get; set; }
		/// <summary>
		///活动Id 
		/// </summary>
		public int? ActiveId { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Book_Catolog
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? SubjectID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Pid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Com_EvaBase
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///类型  0宿舍；1德育 
		/// </summary>
		public Byte? Type { get; set; }
		/// <summary>
		///考评名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///考评内容 
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		///考评周期  0周，1月，2年 
		/// </summary>
		public Byte? Cycle { get; set; }
		/// <summary>
		///分值 
		/// </summary>
		public int? Score { get; set; }
		/// <summary>
		///考评对象  0宿舍；1学生 
		/// </summary>
		public Byte? Target { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Course_Att_Dic
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Score { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EditTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Com_EvaScore
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///模板Id 
		/// </summary>
		public int? TempId { get; set; }
		/// <summary>
		///评分项Id 
		/// </summary>
		public int? BaseId { get; set; }
		/// <summary>
		///考评对象Id 
		/// </summary>
		public int? RelationId { get; set; }
		/// <summary>
		///分值 
		/// </summary>
		public int? Score { get; set; }
		/// <summary>
		///评分年月 
		/// </summary>
		public DateTime? ScoreTime { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Com_EvaScoreset
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///类型  0宿舍；1德育 
		/// </summary>
		public Byte? Type { get; set; }
		/// <summary>
		///预警级别   优秀/良好/警告/劝退 
		/// </summary>
		public string Level { get; set; }
		/// <summary>
		///最大值 
		/// </summary>
		public int? MaxScore { get; set; }
		/// <summary>
		///最小值 
		/// </summary>
		public int? MinScore { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Com_EvaTemp
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///类型  0宿舍；1德育 
		/// </summary>
		public Byte? Type { get; set; }
		/// <summary>
		///模板名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///模板说明 
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		///考评周期  0周，1月，2年 
		/// </summary>
		public Byte? Cycle { get; set; }
		/// <summary>
		///适用范围  0宿舍；1学生 
		/// </summary>
		public Byte? ApplyRange { get; set; }
		/// <summary>
		///评分项 (考评基础项Id字符串) 
		/// </summary>
		public string ScoreItem { get; set; }
		/// <summary>
		///学年学期 
		/// </summary>
		public string LearnYear { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Com_GoodClick
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///类型  0资讯；1相册；2资讯评论 
		/// </summary>
		public Byte? Type { get; set; }
		/// <summary>
		///关联表Id 
		/// </summary>
		public int? RelationId { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///点赞状态 0点赞；1取消点赞 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Com_NewComment
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///资讯Id 
		/// </summary>
		public int? NewId { get; set; }
		/// <summary>
		///父Id 
		/// </summary>
		public int? ParentId { get; set; }
		/// <summary>
		///评论内容 
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		///点赞量 
		/// </summary>
		public int? GoodCount { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Acti_DepartHisLeader
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///部门Id 
		/// </summary>
		public int? DepartId { get; set; }
		/// <summary>
		///类型   0部长；1副部长 
		/// </summary>
		public Byte? Type { get; set; }
		/// <summary>
		///部长/副部长 
		/// </summary>
		public string OSLeaderNo { get; set; }
		/// <summary>
		///上任时间 
		/// </summary>
		public DateTime? OfficeTime { get; set; }
		/// <summary>
		///卸任时间 
		/// </summary>
		public DateTime? OutgoingTime { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Com_NewInfo
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///类型 
		/// </summary>
		public Byte? Type { get; set; }
		/// <summary>
		///关联Id 
		/// </summary>
		public int? RelationId { get; set; }
		/// <summary>
		///资讯类型  0帖子；1通知 
		/// </summary>
		public Byte? NewType { get; set; }
		/// <summary>
		///标题 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///资讯内容 
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		///点击量 
		/// </summary>
		public int? ClickCount { get; set; }
		/// <summary>
		///关联资料 
		/// </summary>
		public int? DocId { get; set; }
		/// <summary>
		///是否置顶  0不置顶(默认)；1置顶 
		/// </summary>
		public Byte? IsTop { get; set; }
		/// <summary>
		///是否精华 0不是精华(默认)；1精华 
		/// </summary>
		public Byte? IsElite { get; set; }
		/// <summary>
		///浏览次数 
		/// </summary>
		public int? BrowsingTimes { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Acti_Activity
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///发起部门 
		/// </summary>
		public int? DepartId { get; set; }
		/// <summary>
		///发起人 
		/// </summary>
		public string OrganizeUserNO { get; set; }
		/// <summary>
		///活动名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///活动范围  以逗号连接的年级Id字符串 
		/// </summary>
		public string Range { get; set; }
		/// <summary>
		///活动类型 0招新；1其他 
		/// </summary>
		public Byte? Type { get; set; }
		/// <summary>
		///活动开始日期 
		/// </summary>
		public DateTime? ActStartTime { get; set; }
		/// <summary>
		///活动结束日期 
		/// </summary>
		public DateTime? ActEndTime { get; set; }
		/// <summary>
		///活动简介 
		/// </summary>
		public string Introduction { get; set; }
		/// <summary>
		///报名开始日期 
		/// </summary>
		public DateTime? EntStartTime { get; set; }
		/// <summary>
		///报名截止日期 
		/// </summary>
		public DateTime? EntEndTime { get; set; }
		/// <summary>
		///活动地址 
		/// </summary>
		public string Address { get; set; }
		/// <summary>
		///学年学期 
		/// </summary>
		public string LearnYear { get; set; }
		/// <summary>
		///奖项说明 
		/// </summary>
		public string Awards { get; set; }
		/// <summary>
		///送审状态  1未送审（默认）2 已送审 
		/// </summary>
		public Byte? SendExamStatus { get; set; }
		/// <summary>
		///审核人 
		/// </summary>
		public string ExamUserNo { get; set; }
		/// <summary>
		///审核意见 
		/// </summary>
		public string ExamSuggest { get; set; }
		/// <summary>
		///审核状态 1待审核（默认）2审核通过 3审核拒绝 
		/// </summary>
		public Byte? ExamStatus { get; set; }
		/// <summary>
		///附件 
		/// </summary>
		public string AttachUrl { get; set; }
		/// <summary>
		///活动图片 
		/// </summary>
		public string ActivityImg { get; set; }
		/// <summary>
		///浏览次数 
		/// </summary>
		public int? BrowsingTimes { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Dorm_Activity
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///宿舍Id 
		/// </summary>
		public int? RoomId { get; set; }
		/// <summary>
		///活动名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///开始时间 
		/// </summary>
		public DateTime? StartTime { get; set; }
		/// <summary>
		///结束时间 
		/// </summary>
		public DateTime? EndTime { get; set; }
		/// <summary>
		///活动地址 
		/// </summary>
		public string Address { get; set; }
		/// <summary>
		///活动内容 
		/// </summary>
		public string Content { get; set; }
		/// <summary>
		///参加成员 
		/// </summary>
		public string JoinMembers { get; set; }
		/// <summary>
		///附件 
		/// </summary>
		public string AttachUrl { get; set; }
		/// <summary>
		///活动图片 
		/// </summary>
		public string ActivityImg { get; set; }
		/// <summary>
		///浏览次数 
		/// </summary>
		public int? BrowsingTimes { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Dorm_Building
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///宿舍楼名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///楼层数 
		/// </summary>
		public Byte? FloorCount { get; set; }
		/// <summary>
		///宿舍楼类型 0男宿舍楼;1女宿舍楼 
		/// </summary>
		public Byte? Type { get; set; }
		/// <summary>
		///负责人 
		/// </summary>
		public string ManagerNo { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Dorm_LeaveRecord
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///宿舍Id 
		/// </summary>
		public int? RoomId { get; set; }
		/// <summary>
		///请假人 
		/// </summary>
		public string LeaveNo { get; set; }
		/// <summary>
		///开始时间 
		/// </summary>
		public DateTime? StartTime { get; set; }
		/// <summary>
		///结束时间 
		/// </summary>
		public DateTime? EndTime { get; set; }
		/// <summary>
		///请假原因 
		/// </summary>
		public string Reason { get; set; }
		/// <summary>
		///状态  0请假;1销假 
		/// </summary>
		public Byte? Status { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Plat_RoleOfMenu
    {

		/// <summary>
		///角色菜单关系表 主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///角色Id 
		/// </summary>
		public int? RoleId { get; set; }
		/// <summary>
		///菜单Id 
		/// </summary>
		public int? MenuId { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Dorm_RoomStuList
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///宿舍Id 
		/// </summary>
		public int? RoomId { get; set; }
		/// <summary>
		///学生 
		/// </summary>
		public string StuNo { get; set; }
		/// <summary>
		///床位号 
		/// </summary>
		public Byte? BedNo { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Plat_RoleOfUser
    {

		/// <summary>
		///角色用户关系表 主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///角色Id 
		/// </summary>
		public int? RoleId { get; set; }
		/// <summary>
		///用户身份证号 
		/// </summary>
		public string UserIDCard { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Plat_Role
    {

		/// <summary>
		///角色表 主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///角色名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///系统Key 
		/// </summary>
		public string SystemKey { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string Creator { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string Editor { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 0正常1删除2归档 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Plat_MenuInfo
    {

		/// <summary>
		///菜单信息表 主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///菜单名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///菜单code 
		/// </summary>
		public string MenuCode { get; set; }
		/// <summary>
		///父级Id 
		/// </summary>
		public int? Pid { get; set; }
		/// <summary>
		///菜单Url 
		/// </summary>
		public string Url { get; set; }
		/// <summary>
		///菜单描述 
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		///是否菜单 0.菜单(默认)；1.按钮 
		/// </summary>
		public Byte? IsMenu { get; set; }
		/// <summary>
		///是否显示菜单  0.不显示;1.显示导航;2.显示权限列表;3.都显示 
		/// </summary>
		public Byte? IsShow { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Plat_Student
    {

		/// <summary>
		///Id 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///身份证件号 
		/// </summary>
		public string IDCard { get; set; }
		/// <summary>
		///学号 
		/// </summary>
		public string SchoolNO { get; set; }
		/// <summary>
		///登录账号 
		/// </summary>
		public string LoginName { get; set; }
		/// <summary>
		///学校ID 
		/// </summary>
		public int? SchoolID { get; set; }
		/// <summary>
		///用户状态 0 启用 1禁用 
		/// </summary>
		public Byte? State { get; set; }
		/// <summary>
		///姓名 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///性别 0男 1女 
		/// </summary>
		public Byte? Sex { get; set; }
		/// <summary>
		///出生日期 
		/// </summary>
		public DateTime? Birthday { get; set; }
		/// <summary>
		///年龄 
		/// </summary>
		public Byte? Age { get; set; }
		/// <summary>
		///照片 
		/// </summary>
		public string Photo { get; set; }
		/// <summary>
		///年级ID 
		/// </summary>
		public int? GradeID { get; set; }
		/// <summary>
		///现住址 
		/// </summary>
		public string Address { get; set; }
		/// <summary>
		///最近登录时间 
		/// </summary>
		public DateTime? LatelyLoginTime { get; set; }
		/// <summary>
		///登录IP 
		/// </summary>
		public string LoginIP { get; set; }
		/// <summary>
		///登录标识码 
		/// </summary>
		public string LoginKey { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? UpdateTime { get; set; }
		/// <summary>
		///备注 
		/// </summary>
		public string Remarks { get; set; }
		/// <summary>
		///昵称 
		/// </summary>
		public string Nickname { get; set; }
		/// <summary>
		///班级ID 
		/// </summary>
		public int? ClassID { get; set; }
		/// <summary>
		///系统Key 
		/// </summary>
		public string SystemKey { get; set; }
		/// <summary>
		///密码 
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		///是否删除 0正常 1删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
		/// <summary>
		///联系电话 
		/// </summary>
		public string Phone { get; set; }
		/// <summary>
		///邮箱 
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		///固定电话 
		/// </summary>
		public string fixPhone { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Plat_Teacher
    {

		/// <summary>
		///Id 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///身份证件号 
		/// </summary>
		public string IDCard { get; set; }
		/// <summary>
		///用户账号 
		/// </summary>
		public string LoginName { get; set; }
		/// <summary>
		///学校Id 
		/// </summary>
		public int? SchoolID { get; set; }
		/// <summary>
		///用户状态  0启用 1禁用 
		/// </summary>
		public Byte? State { get; set; }
		/// <summary>
		///工号 
		/// </summary>
		public string JobNumber { get; set; }
		/// <summary>
		///姓名 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///性别 0男 1女 
		/// </summary>
		public Byte? Sex { get; set; }
		/// <summary>
		///出生日期 
		/// </summary>
		public DateTime? Birthday { get; set; }
		/// <summary>
		///照片 
		/// </summary>
		public string Photo { get; set; }
		/// <summary>
		///现住址 
		/// </summary>
		public string Address { get; set; }
		/// <summary>
		///联系电话 
		/// </summary>
		public string Phone { get; set; }
		/// <summary>
		///备注 
		/// </summary>
		public string Remarks { get; set; }
		/// <summary>
		///最近登录时间 
		/// </summary>
		public DateTime? LatelyLoginTime { get; set; }
		/// <summary>
		///登录IP地址 
		/// </summary>
		public string LoginIP { get; set; }
		/// <summary>
		///登录标识码 
		/// </summary>
		public string LoginKey { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? UpdateTime { get; set; }
		/// <summary>
		///年龄 
		/// </summary>
		public Byte? Age { get; set; }
		/// <summary>
		///昵称 
		/// </summary>
		public string Nickname { get; set; }
		/// <summary>
		///系统Key 
		/// </summary>
		public string SystemKey { get; set; }
		/// <summary>
		///密码 
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		///是否删除 0正常 1删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
		/// <summary>
		///邮箱 
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		///简介 
		/// </summary>
		public string BriefIntroduction { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Sys_UserInfo
    {

		/// <summary>
		/// 
		/// </summary>
		public Byte? UserType { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? Sex { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string LoginName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string UniqueNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? AuthenType { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }
}
