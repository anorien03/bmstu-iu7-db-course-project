

using System.Diagnostics;
using System.Drawing;
using Experiments;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;

using var accessObject = new FillDatabase();
Stopwatch stopwatch = new Stopwatch();

accessObject.Dispose();

stopwatch.Start();
var avgpr = 10;
var maxSize = 4000;
var step = 500;

// var zoneIds = await accessObject.CreateZones(100);
// var packageIds = await accessObject.CreatePackages(100);
//
// foreach (var zoneId in zoneIds)
// {
//     await accessObject.ZoneService.AddPackageAsync(zoneId, packageIds, Role.Administrator);
// }
//
// var userIds = await accessObject.CreateUsers(100);
//
// for (var size = 0; size < maxSize;)
// {
//     await accessObject.CreateBookings(step, userIds, zoneIds, packageIds);
//     size += step;
//     
//     double apptime = 0;
//     for (int j = 0; j < avgpr; j++)
//     {
//         stopwatch.Restart();
//         var bookings = await accessObject.BookingRepository.GetBookingByUserAsync(userIds.First(), Role.Administrator);
//         stopwatch.Stop();
//
//         apptime += stopwatch.ElapsedMilliseconds;
//     }
//
//     Console.WriteLine($"size = {size}, time = {apptime / avgpr} ms");
// }

accessObject.Dispose();
await accessObject.AddLifeboats(100);
await accessObject.AddBodies(100);
await accessObject.AddRangeParticipants(10000);







for (var size = 500; size <= maxSize; size += step)
{
    long apptime = 0;


    await accessObject.RemoveAll();
    await accessObject.AddRangeStatuses(size);


    for (int j = 0; j < avgpr; j++)
    {
        stopwatch.Restart();
        await accessObject.GetAllStatusesByLifeboat(j);

        stopwatch.Stop();
        apptime += stopwatch.ElapsedMilliseconds;
    }

    Console.WriteLine($"size = {size}, time = {apptime / avgpr} ms");
    

    
}

accessObject.Dispose();
Console.ReadLine();
Console.ReadLine();