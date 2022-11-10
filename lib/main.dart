import 'dart:ffi';
import 'dart:io';
import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:youtube_player_flutter/youtube_player_flutter.dart';

List<Game> allGames = [
  Game(
      id: 1,
      name: "The Witcher 3 Wild Hunt",
      types: "RPG,Action,Adventure",
      platforms: "pc",
      metaScore: 93,
      userScore: 9.1,
      image: "https://pbs.twimg.com/media/E531CN9WQAMmK-h.jpg:large",
      video: "LjI8eQuaMyM"),
  Game(
      id: 2,
      name: "Marvels Spider Man Remastered",
      types: "Open-World,Action,Adventure",
      platforms: "pc",
      metaScore: 87,
      userScore: 8.0,
      image:
          "https://uhdwallpapers.org/uploads/converted/18/04/08/spider-man-game-on-ps4-1920x1080_59684-rm-90.jpg",
      video: "K_r4wl2XUng"),
  Game(
      id: 3,
      name: "Cuphead in the Delicious Last Course",
      types: "2D,Action,Platformer",
      platforms: "pc",
      metaScore: 89,
      userScore: 8.4,
      image:
          "https://oyster.ignimgs.com/mediawiki/apis.ign.com/cuphead/9/95/Cuphead_Menu.jpg",
      video: "NN-9SQXoi50"),
  Game(
      id: 4,
      name: "Stray",
      types: "3D,Third-Person,Adventure",
      platforms: "pc",
      metaScore: 83,
      userScore: 8.5,
      image:
          "https://www.ludenoid.com/wp-content/uploads/2022/07/stray_ludenoid_1920x1080.jpg",
      video: "Sk6r51LNFos")
];
void main(List<String> args) {
  runApp(MyApp());
}

class MyApp extends StatefulWidget {
  ///final HttpService httpService = HttpService();
  MyApp({super.key});

  @override
  State<MyApp> createState() => _MyAppState();
}

class _MyAppState extends State<MyApp> {
  List<Game> games = allGames;
  Future<List<Game>> readJsonFile(String filePath) async {
    final String response =
        await rootBundle.loadString('assets/selected-text.json');
    final data = await json.decode(response);
    List<Game> games = [];
    for (var i in data) {
      Game adder = Game(
          name: i["name"],
          id: i["id"],
          image: i["image"],
          metaScore: i["metaScore"],
          platforms: i["platforms"],
          types: i["types"],
          userScore: i["userScore"],
          video: i["video"]);
      games.add(adder);
    }
    return games;
  }

  void searchGame(String query) {
    final suggestions = allGames.where((game) {
      final input = query.toLowerCase();
      final gameName = game.name.toLowerCase();
      final gameTypes = game.types.toLowerCase();
      final gamePlatforms = game.platforms.toLowerCase();
      final gameMetaScore = game.metaScore.toString();
      final gameUserScore = game.userScore.toString();
      return (gameName.contains(input) ||
          gameTypes.contains(input) ||
          gameMetaScore.contains(input) ||
          gameUserScore.contains(input) ||
          gamePlatforms.contains(input));
    }).toList();

    setState(() {
      games = suggestions;
    });
  }

  TextEditingController searchController = TextEditingController();
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
        home: Scaffold(
      appBar: AppBar(
        backgroundColor: Colors.black,
        title: const Text("GameFinder"),
      ),
      body: Column(
        children: [
          TextField(
            controller: searchController,
            onChanged: searchGame,
            decoration: const InputDecoration(
              border: OutlineInputBorder(),
              hintText: 'Enter some game things',
            ),
          ),
          Expanded(
              child: ListView.builder(
                  itemCount: games.length,
                  itemBuilder: ((context, index) {
                    final game = games[index];
                    return game;
                  }))),
        ],
      ),
    ));
  }
}

class Game extends StatelessWidget {
  String name;
  int id;
  String types;
  String platforms;
  int metaScore;
  double userScore;
  String image;
  String video;
  Game(
      {Key? key,
      required this.name,
      required this.id,
      required this.image,
      required this.metaScore,
      required this.platforms,
      required this.types,
      required this.userScore,
      required this.video})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    YoutubePlayerController _controller = YoutubePlayerController(
        initialVideoId: video,
        flags: const YoutubePlayerFlags(
          showLiveFullscreenButton: true,
          autoPlay: false,
          mute: false,
        ));
    return Container(
      margin: EdgeInsets.all(10),
      decoration: BoxDecoration(
          color: Colors.grey,
          border: Border.all(color: Colors.grey),
          borderRadius: BorderRadius.all(Radius.circular(20))),
      width: 100,
      height: 200,
      child: Row(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Container(
            width: 200,
            child: Text(
              "\nName: $name\nTypes: $types\nPlatforms: $platforms\nMetaScore: $metaScore\nUserScore: $userScore\n                                          ",
              textAlign: TextAlign.left,
              style: const TextStyle(fontWeight: FontWeight.bold, height: 1.5),
            ),
          ),
          Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              Image(height: 80, width: 165, image: NetworkImage(image)),
              YoutubePlayer(
                controller: _controller,
                showVideoProgressIndicator: true,
                progressIndicatorColor: Colors.blueAccent,
                width: 165,
              )
            ],
          )
        ],
      ),
    );
  }
}

class MyHttpOverrides extends HttpOverrides {
  @override
  HttpClient createHttpClient(SecurityContext? context) {
    return super.createHttpClient(context)
      ..badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
  }
}
