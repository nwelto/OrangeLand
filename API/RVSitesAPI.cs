using OrangeLand.Data;

namespace OrangeLand.API
{
    public class RVSitesAPI
    {
        public static void Map(WebApplication app)
        {
            // Get rvsites
            app.MapGet("/rvsites", (OrangeLandDbContext db) =>
            {
                var rvSites = db.RVSites.Select(site => new
                {
                    SiteId = site.Id,
                    SiteNumber = site.SiteNumber,
                    HasGrassyArea = site.HasGrassyArea,
                    IsPullThrough = site.IsPullThrough
                }).ToList();

                return Results.Ok(rvSites);
            });

            // Get rvsite by ID
            app.MapGet("/rvsites/{siteId}", (OrangeLandDbContext db, int siteId) =>
            {
                var rvSite = db.RVSites.Find(siteId);

                if (rvSite == null)
                {
                    return Results.NotFound("RV site not found.");
                }

                var siteDetails = new
                {
                    SiteId = rvSite.Id,
                    SiteNumber = rvSite.SiteNumber,
                    HasGrassyArea = rvSite.HasGrassyArea,
                    IsPullThrough = rvSite.IsPullThrough
                };

                return Results.Ok(siteDetails);
            });
        }
    }
}

