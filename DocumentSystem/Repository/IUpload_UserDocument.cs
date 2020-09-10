using DocumentSystem.Models;
using DocumentSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSystem.Repository
{
   public interface IUpload_UserDocument
    {

        Task<long> UploadDocumentPost(Upload_Document upload);
        Task<List<MOrganization>> GetOrganizationList();
        Task<List<MDepartment>> GetDepartmentList(int organizationId);
        Task<List<MUserMaster>> GetUserListByDepartment(int departmentId,string userId);
        Task<List<GetUserDocumentModel>> GetUpload_Documentm(string fromUserIdentityId);
        Task<List<GetUserDocumentModel>> GetAssign_Document(string ToUserIdentityId);

        Task<UserDocumentsResponseModel> GetDocumentById(int id);
        Task<long> UploadUserDocuments(Upload_Document userDocuments);
        //JsonModel GetDocumentById1(Upload_Document userDocuments);
    }
}
