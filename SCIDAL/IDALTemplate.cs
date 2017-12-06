using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCUtility;
using SCModel;
using System.Configuration;
namespace SCIDAL
{

	/// </summary>
	///	
	/// </summary>
    public interface ICourse_ChapterDal: IBaseDal<Course_Chapter>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICouse_ResourceDal: IBaseDal<Couse_Resource>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IActi_DepartHisMemberDal: IBaseDal<Acti_DepartHisMember>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICom_FavoritesDal: IBaseDal<Com_Favorites>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IActi_DepartInfoDal: IBaseDal<Acti_DepartInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IActi_DepartMemberDal: IBaseDal<Acti_DepartMember>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IClassCourseDal: IBaseDal<ClassCourse>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IActi_ProjectDal: IBaseDal<Acti_Project>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICom_MessageDal: IBaseDal<Com_Message>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IModelCatogoryDal: IBaseDal<ModelCatogory>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IActi_RecruitApplyDal: IBaseDal<Acti_RecruitApply>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IActi_WinInfoDal: IBaseDal<Acti_WinInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IModelManageDal: IBaseDal<ModelManage>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IAsso_ActivityDal: IBaseDal<Asso_Activity>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IUserSkinDal: IBaseDal<UserSkin>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICourse_ManageDal: IBaseDal<Course_Manage>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IUser_Model_RelDal: IBaseDal<User_Model_Rel>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IAsso_ApplyDal: IBaseDal<Asso_Apply>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IMyResourceDal: IBaseDal<MyResource>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IResourceTypeDal: IBaseDal<ResourceType>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IAsso_EnteredSetDal: IBaseDal<Asso_EnteredSet>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IResourcesInfoDal: IBaseDal<ResourcesInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IAsso_HisLeaderDal: IBaseDal<Asso_HisLeader>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IDorm_RoomDal: IBaseDal<Dorm_Room>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IAsso_HisMemberDal: IBaseDal<Asso_HisMember>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IAsso_InfoDal: IBaseDal<Asso_Info>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICouse_SelstuinfoDal: IBaseDal<Couse_Selstuinfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IClickDetailDal: IBaseDal<ClickDetail>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IScheduleDal: IBaseDal<Schedule>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IAsso_MemberDal: IBaseDal<Asso_Member>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IAsso_TypeDal: IBaseDal<Asso_Type>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IBook_SubjectDal: IBaseDal<Book_Subject>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICom_ActivityDocDal: IBaseDal<Com_ActivityDoc>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IBook_VersionDal: IBaseDal<Book_Version>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICom_AlbumDal: IBaseDal<Com_Album>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IBook_GradeDal: IBaseDal<Book_Grade>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICourse_AttendanceDal: IBaseDal<Course_Attendance>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICom_AlbumPicDal: IBaseDal<Com_AlbumPic>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IBook_CatologDal: IBaseDal<Book_Catolog>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICom_EvaBaseDal: IBaseDal<Com_EvaBase>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICourse_Att_DicDal: IBaseDal<Course_Att_Dic>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICom_EvaScoreDal: IBaseDal<Com_EvaScore>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICom_EvaScoresetDal: IBaseDal<Com_EvaScoreset>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICom_EvaTempDal: IBaseDal<Com_EvaTemp>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICom_GoodClickDal: IBaseDal<Com_GoodClick>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICom_NewCommentDal: IBaseDal<Com_NewComment>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IActi_DepartHisLeaderDal: IBaseDal<Acti_DepartHisLeader>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ICom_NewInfoDal: IBaseDal<Com_NewInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IActi_ActivityDal: IBaseDal<Acti_Activity>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IDorm_ActivityDal: IBaseDal<Dorm_Activity>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IDorm_BuildingDal: IBaseDal<Dorm_Building>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IDorm_LeaveRecordDal: IBaseDal<Dorm_LeaveRecord>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IPlat_RoleOfMenuDal: IBaseDal<Plat_RoleOfMenu>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IDorm_RoomStuListDal: IBaseDal<Dorm_RoomStuList>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IPlat_RoleOfUserDal: IBaseDal<Plat_RoleOfUser>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IPlat_RoleDal: IBaseDal<Plat_Role>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IPlat_MenuInfoDal: IBaseDal<Plat_MenuInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IPlat_StudentDal: IBaseDal<Plat_Student>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IPlat_TeacherDal: IBaseDal<Plat_Teacher>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ISys_UserInfoDal: IBaseDal<Sys_UserInfo>
    {
		
    }
}