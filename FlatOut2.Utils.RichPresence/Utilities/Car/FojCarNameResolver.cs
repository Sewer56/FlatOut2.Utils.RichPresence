namespace FlatOut2.Utils.RichPresence.Utilities.Car;

public class FojCarNameResolver : ICarNameResolver
{
    private DefaultCarNameResolver _resolver = new();
    
    public string GetName(int id)
    {
        if (id < 45)
            return _resolver.GetName(id);

        // Note: FOJ has car names done entirely in lua without use of DB, so we have to hardcode these.
        var fojCarId = id - 45;
        return fojCarId >= _fojCars.Length ? "" : _fojCars[fojCarId];
    }

    private static readonly string[] _fojCars = 
    {
        "The Machine GT",
        "1974 Road King",
        "1967 Chevelle",
        "67 Shelby GT 500",
        "BMW M3 E92",
        "1957 Chevy",
        "Nissan Skyline",
        "Lotus Elise",
        "1967 Corvette Stingray",
        "1971 Dodge Challenger R/T",
        "1970 Plymouth Hemi CUDA",
        "Lentus GT",
        "Blocker",
        "Viper GTS",
        "Roamer SnowPlow",
        "1985 Nissan Pickup",
        "1985 Ford Ranger",
        "Speedevil",
        "Buick GNX Pro Stock",
        "1923 T-Bucket",
        "1965 Pontiac GTO",
        "Jet Dragster",
        "Racing Tub",
        "Nextel Cup Dodge",
        "Nextel Cup Ford",
        "Soccer Ball",
        "Soccer Derby Ball",
        "Duzenburg",
        "NFS Carbon Cop Car",
        "Pro Street '68 Camaro",
        "Pro Street Zonda",
        "Jet Funny Car",
        "2009 Chevy Camaro 'Vert",
        "2009 Dodge Challenger 'Vert",
        "Little Red Wagon Wheelstander",
        "Racing Shopping Cart",
        "1972 Caddy Hearse",
        "Aston Martin DBR9",
        "MRT",
        "Coke Truck",
        "Pro Street Veyron",
        "Pro Street Chevelle",
        "Pro Street Ford GT",
        "BMW Z4M",
        "Yugo GV",
        "1965 Mustang",
        
        // Page 2
        "Nissan 350Z",
        "1981 Chevy Malibu",
        "Fandom Forty-Nine",
        "GTA Slamvan",
        "Twin Mill",
        "An Escalade",
        "Grave Digger",
        "'09 Dodge Challenger",
        "Custom Insetta Sport",
        "2009 Chevy Camaro",
        "Ice Cream Truck",
        "Pontiac Firebird",
        "Chrysler 300C SRT-8",
        "Dodge Charger SRT-8",
        "1969 GTO Judge",
        "Chopped 55",
        "BMW 3 GTR E46",
        "The Trasher",
        "Dirt Latemodel",
        "RFactor Hammer",
        "GTA Golf Cart",
        "1970 Plymouth GTX",
        "Mini Cooper",
        "Racing Mower",
        "1973 Gremlin",
        "1957 Oldsmobile",
        "1959 Impala",
        "1977 Torino Wagon",
        "1968 RoadRunner",
        "1956 Thunderbird",
        "O3 Mustang",
        "1965 MK1 Lotus Cortina",
        "'68 Corvette",
        "RTrainer",
        "Chopped Rod",
        "T-Rex",
        "Hudson",
        "Ford CCAB",
        "41 Willies",
        "Mob Vert",
        "1936 5 Window Coupe",
        "Ford RS 200",
        "Big Bad AMX",
        "1972 Camaro/Firebird",
        "1999 Viper",
        "Muratus Cobra 427",
        
        // Page 3
        "Murpepper",
        "Unimog",
        "Jeep Hurricane"
    };
}