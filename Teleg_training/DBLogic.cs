using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Teleg_training.DBEntities;
using Teleg_training.Models;
using Teleg_training.Repository;
using Telegram.Bot.Types;

namespace Teleg_training
{
    internal class DBLogic
    {
        IMapper mapper;
        MapperConfiguration config;
        public DBLogic()
        {
            config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMappingProfile());
            });
            mapper = config.CreateMapper();
        }
        public (List<ModelList>, List<ProductModel>) GetLists()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var prodRepository = new ProductRepository(unitOfWork);
                var progRepository = new ProgramListRepository(unitOfWork);
                var authorRepository = new AuthorRepository(unitOfWork);
                var likeRepository = new LikeRepository(unitOfWork);
                var userRepository = new UserRepository(unitOfWork);

                List<DBAuthor> dBauthors = authorRepository.GetAll().ToList();
                List<DBProgramList> dBProgramLists = progRepository.GetAll().ToList();
                List<DBLike> dBLikes = likeRepository.GetAll().ToList();
                List<DBUser> dBUsers = userRepository.GetAll().ToList();

                List<ModelList> listprog = mapper.Map<List<ModelList>>(dBProgramLists);

                List<DBProduct> dBProducts = prodRepository.GetAll().ToList();
                List<ProductModel> listprod = mapper.Map<List<ProductModel>>(dBProducts);
                return (listprog, listprod);

            }
        }
        async public Task<List<ModelList>> GetProgsAsync()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var progRepository = new ProgramListRepository(unitOfWork);
                var authorRepository = new AuthorRepository(unitOfWork);
                var likeRepository = new LikeRepository(unitOfWork);
                var userRepository = new UserRepository(unitOfWork);

                List<DBAuthor> dBauthors = await authorRepository.GetAllAsync();
                List<DBProgramList> dBProgramLists = await progRepository.GetAllAsync();
                List<DBUser> dBUsers = await userRepository.GetAllAsync();
                List<DBLike> likes = await likeRepository.GetAllAsync();

                List<ModelList> listprog = mapper.Map<List<ModelList>>(dBProgramLists);

                return listprog;
            }
        }
        async public Task<List<ProductModel>> GetProdsAsync()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var prodRepository = new ProductRepository(unitOfWork);

                List<DBProduct> dBProducts = await prodRepository.GetAllAsync();
                List<ProductModel> listprod = mapper.Map<List<ProductModel>>(dBProducts);

                return listprod;
            }
        }
        async public Task<string> LikeProgram(ModelList model, long userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var prodRepository = new ProductRepository(unitOfWork);
                var progRepository = new ProgramListRepository(unitOfWork);
                var authorRepository = new AuthorRepository(unitOfWork);
                var likeRepository = new LikeRepository(unitOfWork);
                var userRepository = new UserRepository(unitOfWork);

                List<DBAuthor> dBauthors = await authorRepository.GetAllAsync();
                List<DBProgramList> dBProgramLists = await progRepository.GetAllAsync();
                List<DBUser> dBUsers = await userRepository.GetAllAsync();

                List<DBLike> likes = await likeRepository.GetAllAsync();
                DBProgramList ?prog = await progRepository.GetbyNameAsync(model.Name);
                DBUser ?dBUser = await userRepository.GetByTGIdAsync(userId);
                if (prog != null && dBUser != null)
                {
                    var existingLike = likes.FirstOrDefault(l => l.TGId == userId && l.ProgramListId == prog.ProgramId);
                    if (existingLike != null)
                    {
                        await likeRepository.DeleteAsync(existingLike.LikeId);
                        await unitOfWork.SaveAsync();
                        return "unlike";
                    }
                    else
                    {
                        DBLike like = new DBLike
                        {
                            TGId = dBUser.TGId,
                            ProgramListId = prog.ProgramId,
                            User = dBUser,
                            ProgramList = prog
                        };
                        await likeRepository.CreateAsync(like);
                        await unitOfWork.SaveAsync();
                        return "like";
                    }
                }return "Error";
            }
        }
        public string GetInfoLikeProgram(ModelList model, long userId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var progRepository = new ProgramListRepository(unitOfWork);
                var userRepository = new UserRepository(unitOfWork);
                var likeRepository = new LikeRepository(unitOfWork);
                List<DBLike> likes = likeRepository.GetAll().ToList();
                DBProgramList ?prog = progRepository.GetbyName(model.Name);
                if (prog != null )
                {
                    var existingLike = likes.FirstOrDefault(l => l.TGId == userId && l.ProgramListId == prog.ProgramId);
                    DBUser dBUser = userRepository.GetbyTGId(userId);
                    if (existingLike != null)
                        return "unlike";
                    else
                        return "like";
                }
                else
                    return "Error";
            }
        }
        public bool GetInfoUserExist(long userid)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = new UserRepository(unitOfWork);
                DBUser user = userRepository.GetbyTGId(userid);
                if (user == null)
                    return false;
                else
                    return true;
            }
        }
        public void AddUser(long userid, string username)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = new UserRepository(unitOfWork);
                DBUser dBUser = new DBUser();
                dBUser.TGId = userid;
                dBUser.Name = username;
                userRepository.Create(dBUser);
                unitOfWork.Save();
            }
        }
        public string GetTop()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var progRepository = new ProgramListRepository(unitOfWork);
                var authorRepository = new AuthorRepository(unitOfWork);
                var likeRepository = new LikeRepository(unitOfWork);
                var userRepository = new UserRepository(unitOfWork);
                List<DBAuthor> dBauthors = authorRepository.GetAll().ToList();
                List<DBProgramList> dBProgramLists = progRepository.GetAll().ToList();
                List<DBUser> dBUsers = userRepository.GetAll().ToList();
                List<DBLike> likes = likeRepository.GetAll().ToList();

                List<ModelList> listprog = mapper.Map<List<ModelList>>(dBProgramLists);
                List<ModelList> listnew = listprog.OrderByDescending(o => o.Likes).ToList();
                string progs = "Top list of programs";
                int j = 1;
                for (int i = 0; i < listnew.Count && i < 5; i++)
                {
                    progs += $"\n{j}: {listnew[i].Name}. Author: {listnew[i].Author}. Difficult: {listnew[i].Difficult.ToString()}★. Count of likes: {listnew[i].Likes}❤.Program: /{listnew[i].CodeName}\n";
                    j++;
                }
                return progs;
            }
        }
        public string GetLikedLists(long userid)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var authorRepository = new AuthorRepository(unitOfWork);
                var progRepository = new ProgramListRepository(unitOfWork);
                var likeRepository = new LikeRepository(unitOfWork);
                var userRepository = new UserRepository(unitOfWork);
                List<DBAuthor> dBauthors = authorRepository.GetAll().ToList();
                List<DBProgramList> dBProgramLists = progRepository.GetAll().ToList();
                List<DBUser> dBUsers = userRepository.GetAll().ToList();
                List<DBLike> likes = likeRepository.GetAll().ToList();
                DBUser user = userRepository.GetbyTGId(userid);
                List<DBProgramList> list = new List<DBProgramList>();
                for (int i = 0; i < likes.Count; i++)
                {
                    if (likes[i].TGId == userid)
                        list.Add(likes[i].ProgramList);
                }
                List<ModelList> listnew = mapper.Map<List<ModelList>>(list);
                string progs = "Liked programs";
                int j = 1;
                for (int i = 0; i < listnew.Count; i++)
                {
                    progs += $"\n{j}: {listnew[i].Name}. Author: {listnew[i].Author}. Difficult: {listnew[i].Difficult.ToString()}★. Count of likes: {listnew[i].Likes}❤.Program: /{listnew[i].CodeName}\n";
                    j++;
                }
                return progs;
            }
        }
        public string GetStringListOfPrograms(string gender, string mode)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var authorRepository = new AuthorRepository(unitOfWork);
                var progRepository = new ProgramListRepository(unitOfWork);
                var likeRepository = new LikeRepository(unitOfWork);
                List<DBAuthor> dBauthors = authorRepository.GetAll().ToList();
                List<DBProgramList> dBProgramLists = progRepository.GetAll().ToList();
                List<DBLike> likes = likeRepository.GetAll().ToList();
                List<ModelList> programlist = mapper.Map<List<ModelList>>(dBProgramLists);
                string progs = "";
                if (mode == "mid")
                    progs = "List of programs for amateur";
                else if (mode == "pro")
                    progs = "List of programs for pro";
                else if (mode == "start")
                    progs = "List of programs for begginers";
                if (programlist != null && programlist.Count > 0)
                {
                    int count = 0;
                    for (int i = 0; i < programlist.Count; i++)
                    {
                        if (programlist[i].Gender == gender && programlist[i].Mode == mode)
                        {
                            count++;
                            int j = i + 1;
                            progs += $"\n{j}: {programlist[i].Name}. Author: {programlist[i].Author}. Difficult: {programlist[i].Difficult.ToString()}★. Description: {programlist[i].Description}. Count of likes: {programlist[i].Likes}❤. Program: /{programlist[i].CodeName}";
                        }
                    }
                    if (count == 0)
                        progs = "No programs yet";
                    return progs;
                }
                else
                    progs = "No programs yet";
                return progs;
            }
        }
        public string GetProducts()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var prodRepository = new ProductRepository(unitOfWork);
                List<DBProduct> dBProducts = prodRepository.GetAll().ToList();
                List<ProductModel> productlist = mapper.Map<List<ProductModel>>(dBProducts);
                string prod = "List of products\n";
                if (productlist != null && productlist.Count > 0)
                {
                    for (int i = 0; i < productlist.Count; i++)
                    {
                        prod += productlist[i].Name + "\nDescription: " + productlist[i].Description + "\n" + $"Calories: {productlist[i].Calories}\tProteins: {productlist[i].Proteins}\tFats: {productlist[i].Fats}\tCarbohydrates: {productlist[i].Carbohydrates}\n\n";
                    }
                }
                else
                    prod = "No products yet\n";
                return prod;
            }
        }
        public ModelList GetProgram(string lastmessage)
        {
            using (var unitOfWork = new UnitOfWork())
            {               
                var progRepository = new ProgramListRepository(unitOfWork);
                var authorRepository = new AuthorRepository(unitOfWork);
                var likeRepository = new LikeRepository(unitOfWork);
                var userRepository = new UserRepository(unitOfWork);

                List<DBAuthor> dBauthors = authorRepository.GetAll().ToList();
                List<DBProgramList> dBProgramLists = progRepository.GetAll().ToList();
                List<DBLike> dBLikes = likeRepository.GetAll().ToList();
                List<DBUser> dBUsers = userRepository.GetAll().ToList();
                List<ModelList> programlist = mapper.Map<List<ModelList>>(dBProgramLists);

                return programlist.Find(x => x.CodeName == lastmessage.Substring(1));

            }
        }
    }
}
