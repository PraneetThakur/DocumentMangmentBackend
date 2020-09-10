using DocumentSystem.Data;
using DocumentSystem.Models;
using DocumentSystem.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSystem.Repository
{
    public class cl_Upload_UserDocument : IUpload_UserDocument
    {
        DocumentDBContext db;
        public cl_Upload_UserDocument(DocumentDBContext _db)
        {
            db = _db;
        }

        public async Task<List<MDepartment>> GetDepartmentList(int organizationId)
        {

            if (db != null)
            {
                return await (from p in db.MDepartment
                              from c in db.MOrganization
                              where p.OrganizationId == c.OrganizationId && p.OrganizationId == organizationId
                              select new MDepartment
                              {
                                  DepartmentDisc = p.DepartmentDisc,
                                  DepartmentName = p.DepartmentName,
                                  DeptId = p.DeptId,
                                  IsDeleted = p.IsDeleted,
                                  CreatedDate = p.CreatedDate
                              }).ToListAsync();
            }
            return null;

        }

        public async Task<List<MOrganization>> GetOrganizationList()
        {
            if (db != null)
            {
                return await (from p in db.MOrganization
                              where p.IsDeleted == false
                              select new MOrganization
                              {
                                  OrganizationId = p.OrganizationId,
                                  OrganizationName = p.OrganizationName,
                                  OrganizationAdd = p.OrganizationAdd,
                                  IsDeleted = p.IsDeleted,
                                  CreatedDate = p.CreatedDate
                              }).ToListAsync();
            }
            return null;
        }

        public async Task<List<MUserMaster>> GetUserListByDepartment(int departmentId, string userId)
        {
            if (db != null)
            {
                var user = db.MUserMaster.Where(a => a.IdentityIds == userId).FirstOrDefault();
                if (!object.ReferenceEquals(null, user))
                {
                    return await (from p in db.MUserMaster
                                  where p.DeptId == departmentId && p.UserId!= user.UserId
                                  select new MUserMaster
                                  {
                                      UserId = p.UserId,
                                      OrganizationId = p.OrganizationId,
                                      FirstName = p.FirstName,
                                      LastName = p.LastName,
                                      UserName = p.UserName,
                                      IsDeleted = p.IsDeleted,
                                      CreatedDate = p.CreatedDate
                                  }).ToListAsync();
                }
                  
            }
            return null;
        }

        public async Task<long> UploadDocumentPost(Upload_Document upload)
        {
            if (db != null)
            {
                TDocumentDetails obj = new TDocumentDetails();
                obj.DocumentName = upload.DoucmentName;
                obj.DoucmentType = upload.Extension;
                //obj.DocumentNo = upload.DoucmentNo;
                //obj.DocumentDate = upload.documentDate;
                await db.TDocumentDetails.AddAsync(obj);
                await db.SaveChangesAsync();

                return obj.DocumentId;
            }

            return 0;
        }

        public async Task<List<GetUserDocumentModel>> GetUpload_Documentm(string fromUserIdentityId)
        {
            var user = db.MUserMaster.Where(a => a.IdentityIds == fromUserIdentityId).FirstOrDefault();
            if (!object.ReferenceEquals(null, user))
            {
                return await (from p in db.TDocumentRecord
                              join c in db.TDocumentDetails on p.DocumentRecordId equals c.DocumentRecordid
                              join d in db.MUserMaster on p.FromId equals d.UserId
                              join e in db.MUserMaster on p.ToId equals e.UserId
                              where p.FromId == user.UserId
                              select new GetUserDocumentModel
                              {
                                  DocumentRecordid = p.DocumentRecordId,
                                  Comment = p.Comment,
                                  FromId = p.FromId,
                                  ToId = p.ToId,
                                  FromUser = d.FirstName + " " + d.LastName,
                                  ToUser = e.FirstName + " " + e.LastName,
                                  CreatedDate = p.CreatedDate,
                                  upload_Documents = c
                              }).ToListAsync();
            }
            else return null;
           



        }

        public async Task<List<GetUserDocumentModel>> GetAssign_Document(string ToUserIdentityId)
        {
            var user = db.MUserMaster.Where(a => a.IdentityIds == ToUserIdentityId).FirstOrDefault();
            if (!object.ReferenceEquals(null, user))
            {
                return await (from p in db.TDocumentRecord
                              join c in db.TDocumentDetails on p.DocumentRecordId equals c.DocumentRecordid
                              join d in db.MUserMaster on p.FromId equals d.UserId
                              join e in db.MUserMaster on p.ToId equals e.UserId
                              where p.ToId == user.UserId
                              select new GetUserDocumentModel
                              {
                                  DocumentRecordid = p.DocumentRecordId,
                                  Comment = p.Comment,
                                  FromId = p.FromId,
                                  ToId = p.ToId,
                                  FromUser = d.FirstName + " " + d.LastName,
                                  ToUser = e.FirstName + " " + e.LastName,
                                  CreatedDate = p.CreatedDate,
                                  upload_Documents = c
                              }).ToListAsync();
            }
            else return null;




        }


        public async Task<long> UploadUserDocuments(Upload_Document userDocuments)
        {
            if (db != null)
            {
                var user = db.MUserMaster.Where(a => a.IdentityIds == userDocuments.FromUserId).FirstOrDefault();
                if (!object.ReferenceEquals(null, user))
                {
                    TDocumentRecord docrec = new TDocumentRecord();
                docrec.FromId = user.UserId;
                docrec.ToId = userDocuments.ToUserId;
                docrec.Comment = userDocuments.comment;
                await db.AddAsync(docrec);
                await db.SaveChangesAsync();


                List<TDocumentDetails> DocumentDetails = new List<TDocumentDetails>();

                #region saveDoc
                foreach (var item in userDocuments.Base64)
                {
                    TDocumentDetails userDoc = new TDocumentDetails();

                    item.Value.Replace("\"", "");
                    string[] extensionArr = { "jpg", "jpeg", "png", "txt", "docx", "doc", "xlsx", "pdf", "pptx" };
                    //getting data from base64 url
                    string base64Data = item.Value.Replace("\"", "").Split(':')[0].ToString().Trim();
                    //getting extension of the image
                    string extension = item.Value.Replace("\"", "").Split(':')[1].ToString().Trim();

                    //out from the loop if document extenstion not exist in list of extensionArr
                    if (!extensionArr.Contains(extension)) { goto Finish; }

                    //create directory
                    //string webRootPath = Directory.GetCurrentDirectory()+ "\\PatientDocuments";
                    string webRootPath = Directory.GetCurrentDirectory();

                    //save folder
                    string DirectoryUrl = "//wwwroot//Documents//";

                    if (!Directory.Exists(webRootPath + DirectoryUrl))
                    {
                        Directory.CreateDirectory(webRootPath + DirectoryUrl);
                    }

                    string fileName = userDocuments.DoucmentName + "_" + DateTime.UtcNow.TimeOfDay.ToString();

                    //update file name remove unsupported attr.
                    fileName = fileName.Replace(" ", "_").Replace(":", "_");

                    //create path for save location
                    string path = webRootPath + DirectoryUrl + fileName + "." + extension;

                    //convert files into base
                    Byte[] bytes = Convert.FromBase64String(base64Data);
                    //save int the directory
                    File.WriteAllBytes(path, bytes);



                    //create db path
                    //string uploadPath = @"/Documents/ClientDocuments/" + fileName + "." + extension;
                    userDoc.DocumentRecordid = docrec.DocumentRecordId;
                    userDoc.CreatedBy =Convert.ToInt32( user.UserId);
                        userDoc.CreatedDate = DateTime.UtcNow;
                    userDoc.CreatedDate = DateTime.UtcNow;
                    userDoc.UploadPath = fileName + "." + extension;
                    userDoc.DoucmentType = Path.GetExtension(userDoc.UploadPath); ;

                    //userDoc.Key = userDocuments.Key;
                    DocumentDetails.Add(userDoc);

                }
                //save into db
                await db.TDocumentDetails.AddRangeAsync(DocumentDetails);
                await db.SaveChangesAsync();

                return docrec.DocumentRecordId;
            #endregion

            //return with invaild format message
            Finish:;
                return 0;
            }
            }

            return 0;

        }



        public async Task<UserDocumentsResponseModel> GetDocumentById(int id)
        {
            if (db != null)
            {
                var data = await (from p in db.TDocumentDetails
                                  where p.DocumentId == id
                                  select new TDocumentDetails
                                  {
                                      DocumentRecordid = p.DocumentRecordid,
                                      DoucmentType = p.DoucmentType,
                                      DocumentDate = p.DocumentDate,
                                      CreatedDate = p.CreatedDate,
                                      DocumentId = p.DocumentId,
                                      UploadPath = p.UploadPath
                                  }).FirstOrDefaultAsync();
                string DirectoryUrl = "//wwwroot//Documents//";



                if (File.Exists(Directory.GetCurrentDirectory() + DirectoryUrl + data.UploadPath))
                {
                    UserDocumentsResponseModel userDocumentModel = new UserDocumentsResponseModel();

                    string base64string = Directory.GetCurrentDirectory() + DirectoryUrl + data.UploadPath;

                    Byte[] bytes = File.ReadAllBytes(base64string);
                    MemoryStream memoryFile = new MemoryStream(bytes);
                    String file = Convert.ToBase64String(bytes);

                    userDocumentModel.Base64 = file;

                    userDocumentModel.DocumentTypeName = Path.GetExtension(data.UploadPath);
                    userDocumentModel.File = memoryFile;
                    userDocumentModel.FileName = data.UploadPath;


                    return userDocumentModel;
                }
            }
            return null;
        }
        public JsonModel GetDocumentById1(int id)
        {
            try
            {
                UserDocumentsResponseModel userDocumentModel = new UserDocumentsResponseModel();

                var data = (from p in db.TDocumentDetails
                            where p.DocumentId == id
                            select new TDocumentDetails
                            {
                                DocumentRecordid = p.DocumentRecordid,
                                DoucmentType = p.DoucmentType,
                                DocumentDate = p.DocumentDate,
                                CreatedDate = p.CreatedDate,
                                DocumentId = p.DocumentId,
                                UploadPath = p.UploadPath
                            }).FirstOrDefault();
                string DirectoryUrl = "/Documents";


                if (File.Exists(Directory.GetCurrentDirectory() + DirectoryUrl + data.UploadPath))
                {

                    string base64string = Directory.GetCurrentDirectory() + DirectoryUrl + data.UploadPath;

                    Byte[] bytes = File.ReadAllBytes(base64string);
                    MemoryStream memoryFile = new MemoryStream(bytes);
                    String file = Convert.ToBase64String(bytes);

                    ;

                    userDocumentModel.DocumentTypeName = Path.GetExtension(data.UploadPath);
                    userDocumentModel.File = memoryFile;
                    userDocumentModel.FileName = data.UploadPath;


                }
                return new JsonModel()
                {
                    data = userDocumentModel,
                    Message = "Success",
                    StatusCode = 400
                };
            }
            catch (Exception ex)
            {
                return new JsonModel()
                {
                    data = new object(),
                    Message = "Internal server error",
                    StatusCode = 500,
                    AppError = ex.Message
                };
            }
        }

    }
}
