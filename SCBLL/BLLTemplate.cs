using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCUtility;
using SCDAL;
using SCModel;
using SCIBLL;
namespace SCBLL
{

	/// </summary>
	///	
	/// </summary>
    public partial class Course_ChapterService:BaseService<Course_Chapter>,ICourse_ChapterService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCourse_ChapterDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Couse_ResourceService:BaseService<Couse_Resource>,ICouse_ResourceService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCouse_ResourceDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Acti_DepartHisMemberService:BaseService<Acti_DepartHisMember>,IActi_DepartHisMemberService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetActi_DepartHisMemberDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Com_FavoritesService:BaseService<Com_Favorites>,ICom_FavoritesService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCom_FavoritesDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Acti_DepartInfoService:BaseService<Acti_DepartInfo>,IActi_DepartInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetActi_DepartInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Acti_DepartMemberService:BaseService<Acti_DepartMember>,IActi_DepartMemberService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetActi_DepartMemberDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class ClassCourseService:BaseService<ClassCourse>,IClassCourseService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetClassCourseDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Acti_ProjectService:BaseService<Acti_Project>,IActi_ProjectService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetActi_ProjectDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Com_MessageService:BaseService<Com_Message>,ICom_MessageService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCom_MessageDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class ModelCatogoryService:BaseService<ModelCatogory>,IModelCatogoryService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetModelCatogoryDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Acti_RecruitApplyService:BaseService<Acti_RecruitApply>,IActi_RecruitApplyService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetActi_RecruitApplyDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Acti_WinInfoService:BaseService<Acti_WinInfo>,IActi_WinInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetActi_WinInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class ModelManageService:BaseService<ModelManage>,IModelManageService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetModelManageDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Asso_ActivityService:BaseService<Asso_Activity>,IAsso_ActivityService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetAsso_ActivityDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class UserSkinService:BaseService<UserSkin>,IUserSkinService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetUserSkinDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Course_ManageService:BaseService<Course_Manage>,ICourse_ManageService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCourse_ManageDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class User_Model_RelService:BaseService<User_Model_Rel>,IUser_Model_RelService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetUser_Model_RelDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Asso_ApplyService:BaseService<Asso_Apply>,IAsso_ApplyService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetAsso_ApplyDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class MyResourceService:BaseService<MyResource>,IMyResourceService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetMyResourceDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class ResourceTypeService:BaseService<ResourceType>,IResourceTypeService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetResourceTypeDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Asso_EnteredSetService:BaseService<Asso_EnteredSet>,IAsso_EnteredSetService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetAsso_EnteredSetDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class ResourcesInfoService:BaseService<ResourcesInfo>,IResourcesInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetResourcesInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Asso_HisLeaderService:BaseService<Asso_HisLeader>,IAsso_HisLeaderService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetAsso_HisLeaderDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Dorm_RoomService:BaseService<Dorm_Room>,IDorm_RoomService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetDorm_RoomDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Asso_HisMemberService:BaseService<Asso_HisMember>,IAsso_HisMemberService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetAsso_HisMemberDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Asso_InfoService:BaseService<Asso_Info>,IAsso_InfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetAsso_InfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Couse_SelstuinfoService:BaseService<Couse_Selstuinfo>,ICouse_SelstuinfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCouse_SelstuinfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class ClickDetailService:BaseService<ClickDetail>,IClickDetailService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetClickDetailDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class ScheduleService:BaseService<Schedule>,IScheduleService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetScheduleDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Asso_MemberService:BaseService<Asso_Member>,IAsso_MemberService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetAsso_MemberDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Asso_TypeService:BaseService<Asso_Type>,IAsso_TypeService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetAsso_TypeDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Book_SubjectService:BaseService<Book_Subject>,IBook_SubjectService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetBook_SubjectDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Com_ActivityDocService:BaseService<Com_ActivityDoc>,ICom_ActivityDocService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCom_ActivityDocDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Book_VersionService:BaseService<Book_Version>,IBook_VersionService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetBook_VersionDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Com_AlbumService:BaseService<Com_Album>,ICom_AlbumService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCom_AlbumDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Book_GradeService:BaseService<Book_Grade>,IBook_GradeService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetBook_GradeDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Course_AttendanceService:BaseService<Course_Attendance>,ICourse_AttendanceService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCourse_AttendanceDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Com_AlbumPicService:BaseService<Com_AlbumPic>,ICom_AlbumPicService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCom_AlbumPicDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Book_CatologService:BaseService<Book_Catolog>,IBook_CatologService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetBook_CatologDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Com_EvaBaseService:BaseService<Com_EvaBase>,ICom_EvaBaseService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCom_EvaBaseDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Course_Att_DicService:BaseService<Course_Att_Dic>,ICourse_Att_DicService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCourse_Att_DicDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Com_EvaScoreService:BaseService<Com_EvaScore>,ICom_EvaScoreService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCom_EvaScoreDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Com_EvaScoresetService:BaseService<Com_EvaScoreset>,ICom_EvaScoresetService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCom_EvaScoresetDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Com_EvaTempService:BaseService<Com_EvaTemp>,ICom_EvaTempService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCom_EvaTempDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Com_GoodClickService:BaseService<Com_GoodClick>,ICom_GoodClickService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCom_GoodClickDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Com_NewCommentService:BaseService<Com_NewComment>,ICom_NewCommentService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCom_NewCommentDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Acti_DepartHisLeaderService:BaseService<Acti_DepartHisLeader>,IActi_DepartHisLeaderService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetActi_DepartHisLeaderDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Com_NewInfoService:BaseService<Com_NewInfo>,ICom_NewInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetCom_NewInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Acti_ActivityService:BaseService<Acti_Activity>,IActi_ActivityService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetActi_ActivityDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Dorm_ActivityService:BaseService<Dorm_Activity>,IDorm_ActivityService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetDorm_ActivityDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Dorm_BuildingService:BaseService<Dorm_Building>,IDorm_BuildingService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetDorm_BuildingDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Dorm_LeaveRecordService:BaseService<Dorm_LeaveRecord>,IDorm_LeaveRecordService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetDorm_LeaveRecordDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Plat_RoleOfMenuService:BaseService<Plat_RoleOfMenu>,IPlat_RoleOfMenuService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetPlat_RoleOfMenuDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Dorm_RoomStuListService:BaseService<Dorm_RoomStuList>,IDorm_RoomStuListService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetDorm_RoomStuListDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Plat_RoleOfUserService:BaseService<Plat_RoleOfUser>,IPlat_RoleOfUserService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetPlat_RoleOfUserDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Plat_RoleService:BaseService<Plat_Role>,IPlat_RoleService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetPlat_RoleDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Plat_MenuInfoService:BaseService<Plat_MenuInfo>,IPlat_MenuInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetPlat_MenuInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Plat_StudentService:BaseService<Plat_Student>,IPlat_StudentService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetPlat_StudentDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Plat_TeacherService:BaseService<Plat_Teacher>,IPlat_TeacherService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetPlat_TeacherDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Sys_UserInfoService:BaseService<Sys_UserInfo>,ISys_UserInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_UserInfoDal();
        }
    }	
}