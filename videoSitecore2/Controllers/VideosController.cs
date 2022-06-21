using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json; 
using videoSitecore2.Models;
using System.Collections;
using ReflectionIT.Mvc.Paging;
using System.Dynamic;
using Microsoft.AspNetCore.Http;

namespace videoSitecore2.Controllers
{
    public class VideosController : Controller
    {
        private readonly VideoMDContext _context;


        public VideosController(VideoMDContext context)
        {
            _context = context;
        }

        // GET: Videos
        /* public async Task<IActionResult> Index()
         {
             return View(await _context.Videos.ToListAsync());
         }*/

        public IEnumerable<MetaDataTitles> getMetaTypeTitle(int? id)

        {

            IEnumerable<MetaDataTitles> result = (from me in _context.MetaData
                          join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                          where me.VideoId.Equals(id)
                          select new MetaDataTitles
                          {
                              Title = ti.Title,
                          }).Distinct().ToList();

          
            return (result);

        }


        public Task<PaginatedList<Videoview>> GetAllVideo(string searchString, int? pageNumber, int metaDataType)

        {

            //  int SelectedValue = metaDataTypes.Id;
            //  ViewBag.SelectedValue = metaDataTypes.Id;
            ViewBag.SelectedValue = metaDataType;

            List<MetaDataTypes> list = new List<MetaDataTypes>();
            list = (from me in _context.MetaDataTypes select me).ToList();
            list.Insert(0, new MetaDataTypes { Id = 0, Name = "select" });
            ViewBag.ListofMetaDataType = list;


            ViewData["metaDataTypes"] = metaDataType;
            ViewData["SearchString"] = searchString;


            IQueryable<Videoview> result = (from vi in _context.Videos
                                            join me in _context.MetaData on vi.Id equals me.VideoId
                                            join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                                            select new Videoview

                                            {
                                                Id = vi.Id,
                                                Name = vi.Name,
                                                Location = vi.Location,
                                                Description = vi.Description,
                                                Title = ti.Title,
                                                metaTypeName = ti.MetaDataType.Name,
                                                metaDataTypeId = ti.MetaDataType.Id,
                                                metadataTitleId = me.MetadataTitleId,

                                            }).Distinct().OrderBy(vi => vi.Id);

            Console.WriteLine("result 111 size is  " + result.Count());
            HttpContext.Items["result"] = result;

            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;
                result = result.Where(res => res.Title.Contains(searchString) || res.Name.Contains(searchString) || res.Description.Contains(searchString));

            }
            Console.WriteLine("result 222 size is  " + result.Count());

            if (metaDataType != 0)
            {
                pageNumber = 1;
                result = result.Where(me => me.metaDataTypeId == metaDataType);
            }
            Console.WriteLine("result 333 size is  " + result.Count());

            //  result = result.Distinct().OrderBy(vi => vi.Id);

            Console.WriteLine("result 444 size is  " + result.Count());
            var result2 = result.AsNoTracking().GroupBy(d => d.Id).Select(d => new Videoview
            {
                Id = d.Key,
                Name = d.Min(k => k.Name)
            });
            int pageSize = 10;
            return PaginatedList<Videoview>.CreateAsync(result2, pageNumber ?? 1, pageSize);

        }


      /*  public ActionResult IndexViewModel(int id)
        {
            ViewBag.Message = "Welcome to my demo!";
            ViewModel mymodel = new ViewModel();
            mymodel.Teachers = getMetaTypeTitle(id);
            mymodel.Students = IndexAll();
            return View(mymodel);
        }*/


     /*   public ActionResult IndexViewData(int? id, string searchString, int? pageNumber, int metaDataType)
        {
            ViewBag.Message = "Welcome to my demo!";
            ViewData["Titles"] = getMetaTypeTitle(id);
            ViewData["AllVideo"] = GetAllVideo(searchString, pageNumber, metaDataType);
            return View();
        }*/



        public ActionResult Display2()
        {
            return Json(new { foo = "bar", baz = "Blech" });
        }



        // GET: zsm play
        public IActionResult Display22(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            var result = _context.Videos
          .Join(_context.MetaData, v => v.Id, m => m.VideoId,
              ((vid, meta) => new metaVsvideo { videos = vid, metadata = meta })).Where(m => m.metadata.VideoId == id)
          .ToList();

            foreach (var invoice in result)
            {
                Console.WriteLine("VideoId: {0}, StartFrame: {1} " + "Name: {2} ",
                    invoice.metadata.VideoId, invoice.metadata.StartTime, invoice.videos.Name);
            }

            if (result == null)
            {
                Console.WriteLine(" NotFound ");

                return NotFound();
            }

            //var settings = new JsonSerializerSettings();
            // return View(JsonConvert.SerializeObject(result, settings));
            //return Json(new { result });
            // return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            //  return View(result);
            return Json(result, new JsonSerializerOptions
            {
                WriteIndented = true,
            });
        }


        public async Task<IActionResult> player(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            var result = (from vi in _context.Videos
                          join me in _context.MetaData on vi.Id equals me.VideoId
                          join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                          where vi.Id.Equals(id)

                          select new Videoview
                          {
                              Id = vi.Id,
                              Name = vi.Name,
                              Location = vi.Location,
                              Description = vi.Description,
                              Title = ti.Title,
                              metadataTitleId= me.MetadataTitleId
                          }).Distinct().ToList();

            if (result == null)
            {
                Console.WriteLine(" NotFound ");

                return NotFound();
            }
            return View(result);

        }

        public async Task<IActionResult> playVideo(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }

                   var result = (from vi in _context.Videos
                          join me in _context.MetaData on vi.Id equals me.VideoId
                          where vi.Id.Equals(id)

                                 select new Videoview
                                 {
                                     Id = vi.Id,
                                     Name = vi.Name,
                                     Location = vi.Location,
                                     Description = vi.Description,
                                     //  Title = ti.Title,
                                     StartTime = me.StartTime
                                 }).Distinct();

              if (result == null)
            {
                Console.WriteLine(" NotFound ");

                return NotFound();
            }
            return View(await result.ToListAsync());

        }

        // GET: zsm play


        public ActionResult getMetaTypeforVideo(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            var result = ( from me in _context.MetaData 
                          join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                           where me.VideoId.Equals(id)
                           select new MetaDataTitles
                           {
                           Title = ti.Title,
                           }).Distinct().ToList();


            if (result == null)
            {
                Console.WriteLine(" NotFound ");

                return NotFound();
            }
            return View(result);
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

        }


        public ActionResult startTimeEvent2(int? id, int? metadataTitleId)

        {
            if (id == null || metadataTitleId==null)
            {
                return NotFound();
            }

            List<Localisation2> local2 = (from vi in _context.Videos
                                          join me in _context.MetaData on vi.Id equals me.VideoId
                                          join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                                           where vi.Id.Equals(id) && ti.Id.Equals(metadataTitleId)
                                          select new Localisation2
                                          {
                                              tc = me.StartTime,
                                              tclevel = 1,
                                              label = "Start"
                                          }).Distinct().ToList();

            Sublocalisations Sub = new Sublocalisations();
            Sub.localisation = local2;


            /*  Localisation local = new Localisation();
              local.tcin = "00:00:00.0000";
              local.tcout = "00:00:15.0000";
              local.tclevel = 1;
              local.sublocalisations = Sub;*/

            List<Localisation> local = (from vi in _context.Videos
                                        where vi.Id.Equals(id)
                                        select new Localisation
                                        {
                                            type= "events",
                                            tcin = "00:00:00.0000",
                                            tcout = "00:00:15.0000",
                                            tclevel = 1,
                                            sublocalisations = Sub
                                        }).Distinct().ToList();


            Rootjsonclass rootjsonclass = new Rootjsonclass();
            rootjsonclass.localisation = (List<Localisation>)local;
           // rootjsonclass.id = "events-amalia01";
            rootjsonclass.id = metadataTitleId.ToString();
            rootjsonclass.type = "events";
            rootjsonclass.algorithm = "demo-video-generator";
            rootjsonclass.processor = "Ina Research Department - N. HERVE";
            rootjsonclass.processed = 1421141589279;
            rootjsonclass.version = 1;



            if (rootjsonclass == null)
            {
                Console.WriteLine(" NotFound ");

                return NotFound();
            }
            return Json(rootjsonclass);
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

        }



        public ActionResult startTimeEvent(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            List<Localisation2> local2 = (from vi in _context.Videos
                                          join me in _context.MetaData on vi.Id equals me.VideoId
                                          where vi.Id.Equals(id)
                                          select new Localisation2
                                          {
                                              tc = me.StartTime,
                                              tclevel = 1,
                                              label = "Start"
                                          }).Distinct().ToList();

            Sublocalisations Sub = new Sublocalisations();
            Sub.localisation = local2;


           /*  Localisation local = new Localisation();
             local.tcin = "00:00:00.0000";
             local.tcout = "00:00:15.0000";
             local.tclevel = 1;
             local.sublocalisations = Sub;*/

            List<Localisation> local = (from vi in _context.Videos
                                          where vi.Id.Equals(id)
                                          select new Localisation
                                          {
                                              tcin = "00:00:00.0000",
                                              tcout = "00:00:15.0000",
                                              tclevel = 1,
                                            sublocalisations = Sub
                                           }).Distinct().ToList();


            Rootjsonclass rootjsonclass = new Rootjsonclass();
            rootjsonclass.localisation = (List<Localisation>)local;
            rootjsonclass.id = "events-amalia01";
            rootjsonclass.type = "events";
            rootjsonclass.algorithm = "demo-video-generator";
            rootjsonclass.processor = "Ina Research Department - N. HERVE";
            rootjsonclass.processed = 1421141589279;
            rootjsonclass.version = 1;
           


            if (rootjsonclass == null)
            {
                Console.WriteLine(" NotFound ");

                return NotFound();
            }
            return Json(rootjsonclass);
            // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

        }



        public async Task<IActionResult> AllVideo()

        {
           var result = (from vi in _context.Videos
                                            join me in _context.MetaData on vi.Id equals me.VideoId
                                            join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                         //  select new Videos()).Distinct().OrderBy(vi => vi.Id);
                         select new metaVsvideo { videos = vi, metadata = me }).Distinct().OrderBy(vi => vi.Id);



            if (result == null)
            {
                Console.WriteLine(" NotFound ");

                return NotFound();
            }
            return View(result.ToList());

        }



        public ActionResult Display(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }

                   var result = (from vi in _context.Videos
                          join me in _context.MetaData on vi.Id equals me.VideoId
                          where vi.Id.Equals(id)
                          select new
                          {
                              Id = vi.Id,
                              Name = vi.Name,
                              Location = vi.Location,
                              Description = vi.Description,
                              StartTime = me.StartTime
                          }).Distinct().ToList();

                      

            if (result == null)
            {
                Console.WriteLine(" NotFound ");

                return NotFound();
            }
            return Json(result);

        }


        public ActionResult Display_asli(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            /*  var result = _context.Videos
            .Join(_context.MetaData, v => v.Id, m => m.VideoId,
                ((vid, meta) => new metaVsvideo { videos = vid, metadata = meta })).Where(m => m.metadata.VideoId == id)
            .Distinct().ToList(); */

            var result = (from vi in _context.Videos
                          join me in _context.MetaData on vi.Id equals me.VideoId
                          where vi.Id.Equals(id)
                          select new
                          {
                              Id = vi.Id,
                              Name = vi.Name,
                              Location = vi.Location,
                              Description = vi.Description,
                              StartTime = me.StartTime
                          }).Distinct().ToList();



            if (result == null)
            {
                Console.WriteLine(" NotFound ");

                return NotFound();
            }
            return Json(result);

        }


        // add by zsm
        public async Task<IActionResult> Index_sade(string searchString)
        {
            var result = from m in _context.Videos
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(s => s.Name.Contains(searchString));
            }
                       
            return View(await result.ToListAsync());
        }

        // add by zsm



        /*   public ActionResult Index()
           {
               ViewBag.Message = "Welcome to my demo!";
               dynamic mymodel = new ExpandoObject();
               mymodel.Teachers = GetTeachers();
               mymodel.Students = GetStudents();
               return View(mymodel);
           } */




        public JsonResult getTitleList(int? id)

        {
           // List<MetaDataTitles> TitleList = new List<MetaDataTitles>();

          var  TitleList = (from me in _context.MetaData
                          join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                          where me.VideoId.Equals(id)
                          //select new MetaDataTitles
                           select new 
                           {
                            Title = ti.Title,
                          }).Distinct().ToList();

            Console.WriteLine("result is  id " + id+ " Title "+ TitleList.Last());

          //  return Json(TitleList, JsonRequestBehavior.AllowGet);
           return Json(TitleList);

        }



        public async Task<IActionResult> Index_work(string searchString, int? pageNumber , int metaDataType)

        {

            //  int SelectedValue = metaDataTypes.Id;
            //  ViewBag.SelectedValue = metaDataTypes.Id;
            ViewBag.SelectedValue = metaDataType;

             List <MetaDataTypes> list = new List<MetaDataTypes>();
             list = (from me in _context.MetaDataTypes select me).ToList();
             list.Insert(0 , new MetaDataTypes { Id = 0, Name = "select" });
             ViewBag.ListofMetaDataType = list;


             ViewData["metaDataTypes"] = metaDataType;
             ViewData["SearchString"] = searchString;


            IQueryable<Videoview> result = (from vi in _context.Videos
                      join me in _context.MetaData on vi.Id equals me.VideoId
                      join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                      select new Videoview

                      {
                          Id = vi.Id,
                          Name = vi.Name,
                        //  Location = vi.Location,
                          Description = vi.Description,
                         // Title = ti.Title,
                          metaTypeName = ti.MetaDataType.Name,
                         // metaDataTypeId = ti.MetaDataType.Id,
                         // metadataTitleId = me.MetadataTitleId,
                         
                          //zsm
                          //  metaDataTitles = (ICollection<MetaDataTitles>)me.MetadataTitle
                          // metaDataTitles = getMetaTypeTitle(vi.Id)
                        //   metaDataTitles = (List<MetaDataTitles>)ti


                      }).Distinct().OrderBy(vi => vi.Id); 


          //  HttpContext.Items["result"] = result;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;
                result = result.Where(res => res.Title.Contains(searchString) || res.Name.Contains(searchString) || res.Description.Contains(searchString));

            }
          //  Console.WriteLine("result 222 size is  " + result.Count());

            if (metaDataType != 0)
            {
                pageNumber = 1;
               result = result.Where(me => me.metaDataTypeId == metaDataType);
            }
            Console.WriteLine("result 333 size is  " + result.Count());

          //  result = result.Distinct().OrderBy(vi => vi.Id);

            Console.WriteLine("result 444 size is  " + result.Count());
            /* var result2 = result.AsNoTracking().GroupBy(d => d.Id).Select(d => new Videoview
             {
                 Id = d.Key,
                 Name = d.Min(k => k.Name)
             });
             int pageSize = 10;
                return View(await PaginatedList<Videoview>.CreateAsync(result2, pageNumber ?? 1, pageSize)); */
            int pageSize = 10;
            return View(await PaginatedList<Videoview>.CreateAsync(result.AsNoTracking(), pageNumber ?? 1, pageSize));



        }


        public async Task<IActionResult> Index(string searchString, int? pageNumber, int metaDataType)

        {

            //  int SelectedValue = metaDataTypes.Id;
            //  ViewBag.SelectedValue = metaDataTypes.Id;
            ViewBag.SelectedValue = metaDataType;

            List<SelectListItem> list = new List<SelectListItem>();
            var tlist = (from me in _context.MetaDataTypes select me).ToList();
            foreach (var item in tlist)
            {
                var i = new SelectListItem(item.Name, item.Id.ToString());
                if (i.Value.Equals(metaDataType.ToString()))
                    i.Selected = true;
                list.Add(i);
            }
            list.Insert(0, new SelectListItem ("انتخاب ...","0"));
            
            ViewBag.ListofMetaDataType = list;


            ViewData["metaDataType"] = metaDataType;
            ViewData["SearchString"] = searchString;


            IQueryable<Videoview> result = (from vi in _context.Videos
                                            join me in _context.MetaData on vi.Id equals me.VideoId
                                            join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                                            select new Videoview

                                            {
                                                Id = vi.Id,
                                                Name = vi.Name,
                                                // Location = vi.Location,
                                                Description = vi.Description,
                                                Title = ti.Title,
                                                metaTypeName = ti.MetaDataType.Name,
                                                 metaDataTypeId = ti.MetaDataType.Id,
                                                // metadataTitleId = me.MetadataTitleId,
                                                                                              

                                            }).Distinct().OrderBy(vi => vi.Id);



            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;
                result = result.Where(res => res.Title.Contains(searchString) || res.Name.Contains(searchString) || res.Description.Contains(searchString));
                             

            }

            if (metaDataType != 0)
            {
                pageNumber = 1;
                result = result.Where(me => me.metaDataTypeId == metaDataType);
            }

            result = result.Select(d => new Videoview
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                metaTypeName = d.metaTypeName,
                metaDataTypeId = d.metaDataTypeId

            }).Distinct().OrderBy(vi => vi.Id);

            int pageSize = 10;
            return View(await PaginatedList<Videoview>.CreateAsync(result.AsNoTracking(), pageNumber ?? 1, pageSize));
                    }

        public async Task<IActionResult> Index_alavi(string searchString, int? pageNumber, int metaDataType)

        {

            //  int SelectedValue = metaDataTypes.Id;
            //  ViewBag.SelectedValue = metaDataTypes.Id;
            ViewBag.SelectedValue = metaDataType;

            List<MetaDataTypes> list = new List<MetaDataTypes>();
            list = (from me in _context.MetaDataTypes select me).ToList();
            list.Insert(0, new MetaDataTypes { Id = 0, Name = "select" });
            ViewBag.ListofMetaDataType = list;


            ViewData["metaDataTypes"] = metaDataType;
            ViewData["SearchString"] = searchString;


            IQueryable<Videoview> result = (from vi in _context.Videos
                                            join me in _context.MetaData on vi.Id equals me.VideoId
                                            join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                                            select new Videoview

                                            {
                                                Id = vi.Id,
                                                Name = vi.Name,
                                                Location = vi.Location,
                                                Description = vi.Description,
                                                Title = ti.Title,
                                                metaTypeName = ti.MetaDataType.Name,
                                                metaDataTypeId = ti.MetaDataType.Id,
                                                metadataTitleId = me.MetadataTitleId,


                                            }).Distinct().OrderBy(vi => vi.Id);


            //  HttpContext.Items["result"] = result;

            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;
                result = result.Where(res => res.Title.Contains(searchString) || res.Name.Contains(searchString) || res.Description.Contains(searchString));

            }

            if (metaDataType != 0)
            {
                pageNumber = 1;
                result = result.Where(me => me.metaDataTypeId == metaDataType);
            }


            var result2 = result.AsNoTracking().GroupBy(d => d.Id).Select(d => new Videoview
            {
                Id = d.Key,
                Name = d.Min(k => k.Name)
            });
            int pageSize = 10;
            return View(await PaginatedList<Videoview>.CreateAsync(result2, pageNumber ?? 1, pageSize));
            // int pageSize = 10;
            //  return View(await PaginatedList<Videoview>.CreateAsync(result.AsNoTracking(), pageNumber ?? 1, pageSize));



        }


        public async Task<IActionResult> Index_asli990615(string searchString, int? pageNumber, int metaDataType)

        {

            //  int SelectedValue = metaDataTypes.Id;
            //  ViewBag.SelectedValue = metaDataTypes.Id;
            ViewBag.SelectedValue = metaDataType;

            List<MetaDataTypes> list = new List<MetaDataTypes>();
            list = (from me in _context.MetaDataTypes select me).ToList();
            list.Insert(0, new MetaDataTypes { Id = 0, Name = "select" });
            ViewBag.ListofMetaDataType = list;


            ViewData["metaDataTypes"] = metaDataType;

            ViewData["SearchString"] = searchString;
            IQueryable<Videoview> result = null;
            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;

                if (metaDataType != 0)
                {
                    result = (from vi in _context.Videos
                              join me in _context.MetaData on vi.Id equals me.VideoId
                              join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                              where ((vi.Name.Contains(searchString) || vi.Description.Contains(searchString) || ti.Title.Contains(searchString)) && me.MetadataTitleId == metaDataType)
                              select new Videoview
                              {
                                  Id = vi.Id,
                                  Name = vi.Name,
                                  Location = vi.Location,
                                  Description = vi.Description,
                                  Title = ti.Title,
                                  metaTypeName = ti.MetaDataType.Name
                              }).Distinct().OrderBy(vi => vi.Id);
                }
                else
                {
                    result = (from vi in _context.Videos
                              join me in _context.MetaData on vi.Id equals me.VideoId
                              join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                              where (vi.Name.Contains(searchString) || vi.Description.Contains(searchString) || ti.Title.Contains(searchString))
                              select new Videoview
                              {
                                  Id = vi.Id,
                                  Name = vi.Name,
                                  Location = vi.Location,
                                  Description = vi.Description,
                                  Title = ti.Title,
                                  metaTypeName = ti.MetaDataType.Name
                              }).Distinct().OrderBy(vi => vi.Id);
                }
                // if(metaDataType!=0)  result = result.Where(me => me.metadataTitleId == metaDataType);
                //   return View(result.ToList());
                int pageSize = 10;
                return View(await PaginatedList<Videoview>.CreateAsync(result.AsNoTracking(), pageNumber ?? 1, pageSize));

            }

            else
            {

                if (metaDataType != 0)
                {

                    result = (from vi in _context.Videos
                              join me in _context.MetaData on vi.Id equals me.VideoId
                              join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                              where (me.MetadataTitleId == metaDataType)
                              select new Videoview

                              {
                                  Id = vi.Id,
                                  Name = vi.Name,
                                  Location = vi.Location,
                                  Description = vi.Description,
                                  Title = ti.Title,
                                  metaTypeName = ti.MetaDataType.Name

                              }).Distinct().OrderBy(vi => vi.Id);

                }
                else
                {

                    result = (from vi in _context.Videos
                              join me in _context.MetaData on vi.Id equals me.VideoId
                              join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                              select new Videoview

                              {
                                  Id = vi.Id,
                                  Name = vi.Name,
                                  Location = vi.Location,
                                  Description = vi.Description,
                                  Title = ti.Title,
                                  metaTypeName = ti.MetaDataType.Name

                              }).Distinct().OrderBy(vi => vi.Id);
                }

                //  if (metaDataType != 0) result = result.Where(me => me.metadataTitleId == metaDataType);

                //return View(result.ToList());
                int pageSize = 10;
                return View(await PaginatedList<Videoview>.CreateAsync(result.AsNoTracking(), pageNumber ?? 1, pageSize));

            }


        }

        /*  public ActionResult IndexViewBag(string searchString, int? pageNumber, MetaDataTypes metaDataTypes)
          {
              ViewBag.MetaDatas = GetMeta(metaDataTypes);
              ViewBag.Students = IndexView(searchString, pageNumber);
              return View();
          }


          public async Task<IActionResult> GetMeta(MetaDataTypes metaDataTypes)

          {
              int SelectedValue = metaDataTypes.Id;
              ViewBag.SelectedValue = metaDataTypes.Id;


              List<MetaDataTypes> list = new List<MetaDataTypes>();
              list = (from me in _context.MetaDataTypes select me).ToList();
              list.Insert(0, new MetaDataTypes { Id = 0, Name = "select" });
              ViewBag.ListofMetaDataType = list;
          } */

        public async Task<IActionResult> IndexView(string searchString, int? pageNumber , MetaDataTypes metaDataTypes)

        {

          /*  int SelectedValue = metaDataTypes.Id;
             ViewBag.SelectedValue = metaDataTypes.Id;


             List<MetaDataTypes> list = new List<MetaDataTypes>();
             list = (from me in _context.MetaDataTypes select me).ToList();
             list.Insert(0 , new MetaDataTypes { Id = 0, Name = "select" });
             ViewBag.ListofMetaDataType = list; */
            
            ViewData["metaDataTypes"] = metaDataTypes;



            ViewData["SearchString"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                pageNumber = 1;
                IQueryable<Videoview> result = (from vi in _context.Videos
                                                join me in _context.MetaData on vi.Id equals me.VideoId
                                                join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                                                where (vi.Name.Contains(searchString) || vi.Description.Contains(searchString) || ti.Title.Contains(searchString))
                                                select new Videoview
                                                {
                                                    Id = vi.Id,
                                                    Name = vi.Name,
                                                    Location = vi.Location,
                                                    Description = vi.Description,
                                                    Title = ti.Title,
                                                    metaTypeName = ti.MetaDataType.Name
                                                }).Distinct().OrderBy(vi => vi.Id);


                //   return View(result.ToList());
                int pageSize = 10;
                return View(await PaginatedList<Videoview>.CreateAsync(result.AsNoTracking(), pageNumber ?? 1, pageSize));

            }

            else
            {

                IQueryable<Videoview> result = (from vi in _context.Videos
                                                join me in _context.MetaData on vi.Id equals me.VideoId
                                                join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                                                select new Videoview

                                                {
                                                    Id = vi.Id,
                                                    Name = vi.Name,
                                                    Location = vi.Location,
                                                    Description = vi.Description,
                                                    Title = ti.Title,
                                                    metaTypeName = ti.MetaDataType.Name

                                                }).Distinct().OrderBy(vi => vi.Id);



                //return View(result.ToList());
                int pageSize = 10;
                return View(await PaginatedList<Videoview>.CreateAsync(result.AsNoTracking(), pageNumber ?? 1, pageSize));

            }


        }



        public async Task<IActionResult> Index_asli(string searchString)
        {
             if (!String.IsNullOrEmpty(searchString))
            {
                var result = (from vi in _context.Videos
                              join me in _context.MetaData on vi.Id equals me.VideoId
                              join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                              where (vi.Name.Contains(searchString) || vi.Description.Contains(searchString) || ti.Title.Contains(searchString))
                              select  new Videoview
                              {
                                  Id = vi.Id,
                                  Name = vi.Name,
                                  Location = vi.Location,
                                  Description = vi.Description,
                                  Title = ti.Title
                              }).Distinct() ;


                           /* foreach (var invoice in result)
                {
                    Console.WriteLine("VideoId: {0}, StartFrame: {1} " + "Name: {2} ",
                        invoice.Id, invoice.Location, invoice.Name);
                }*/
                return View(result.ToList());
                                            

           }

            else
            {

                var result = (from vi in _context.Videos
                              join me in _context.MetaData on vi.Id equals me.VideoId
                              join ti in _context.MetaDataTitles on me.MetadataTitleId equals ti.Id
                              select new Videoview

                              {
                                  Id = vi.Id,
                                  Name = vi.Name,
                                  Location = vi.Location,
                                  Description = vi.Description,
                                  Title = ti.Title

                              }).Distinct();


                return View(result.ToList());
            }
        }


        // GET: Videos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videos = await _context.Videos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videos == null)
            {
                return NotFound();
            }

            return View(videos);
        }


        public async Task<IActionResult> Play2(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            // var videos = await _context.Videos.FirstOrDefaultAsync(m => m.Id == id);
            //var result = await _context.MetaData.FirstOrDefaultAsync(m => m.VideoId == id);

            var result = _context.Videos
          .Join(_context.MetaData, v => v.Id, m => m.VideoId,
              ((vid, meta) => new metaVsvideo { videos = vid, metadata = meta })).Where(m => m.metadata.VideoId == id)
          .ToList();

            foreach (var invoice in result)
            {
                Console.WriteLine("VideoId: {0}, StartFrame: {1} " + "Name: {2} ",
                    invoice.metadata.VideoId, invoice.metadata.StartTime, invoice.videos.Name);
            }

            //    var result =  _context.MetaData.Where(m => m.VideoId == id).ToList();

            if (result == null)
            {
                Console.WriteLine(" NotFound ");

                return NotFound();
            }

            //var settings = new JsonSerializerSettings();
            // return View(JsonConvert.SerializeObject(result, settings));

            return View(result);
        }


        // GET: zsm play
        public async Task<IActionResult> Play()
        
        {
                        // var videos = await _context.Videos.FirstOrDefaultAsync(m => m.Id == id);
            //var result = await _context.MetaData.FirstOrDefaultAsync(m => m.VideoId == id);
                                    
          var result = _context.Videos
        .Join(_context.MetaData, v => v.Id, m => m.VideoId ,
            ((vid, meta) => new metaVsvideo { videos = vid, metadata = meta }))
        //.Where(m => m.metadata.VideoId == id)
        .ToList();

                    foreach (var invoice in result)
            {
                Console.WriteLine("VideoId: {0}, StartFrame: {1} " + "Name: {2} ",
                    invoice.metadata.VideoId, invoice.metadata.StartTime, invoice.videos.Name);
            }
            
                      //    var result =  _context.MetaData.Where(m => m.VideoId == id).ToList();

            if (result == null)
            {
                Console.WriteLine(" NotFound ");

                return NotFound();
            }

            //var settings = new JsonSerializerSettings();
           // return View(JsonConvert.SerializeObject(result, settings));

            return View(result);
        }

        // GET: Videos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location,Description")] Videos videos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(videos);
        }

        // GET: Videos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videos = await _context.Videos.FindAsync(id);
            if (videos == null)
            {
                return NotFound();
            }
            return View(videos);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,Description")] Videos videos)
        {
            if (id != videos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideosExists(videos.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(videos);
        }

        // GET: Videos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videos = await _context.Videos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videos == null)
            {
                return NotFound();
            }

            return View(videos);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videos = await _context.Videos.FindAsync(id);
            _context.Videos.Remove(videos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideosExists(int id)
        {
            return _context.Videos.Any(e => e.Id == id);
        }
    }


    public class Localisation2
    {
        public string label { get; set; }
        public string tc { get; set; }
        public int tclevel { get; set; }

    }

    public class Sublocalisations
    {
        public List<Localisation2> localisation { get; set; }

    }

    public class Localisation
    {
        public Sublocalisations sublocalisations { get; set; }
        public string type { get; set; }
        public string tcin { get; set; }
        public string tcout { get; set; }
        public int tclevel { get; set; }

    }

    public class Rootjsonclass
    {
        public List<Localisation> localisation { get; set; }
        public string id { get; set; }
        public string type { get; set; }
        public string algorithm { get; set; }
        public string processor { get; set; }
        public long processed { get; set; }
        public int version { get; set; }

    }




}
