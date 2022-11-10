class Post {
  final String name;
  final int id;
  final String types;
  final String platforms;
  final int metaScore;
  final double userScore;
  final String image;
  final String video;

  Post(
      {required this.name,
      required this.id,
      required this.types,
      required this.platforms,
      required this.metaScore,
      required this.userScore,
      required this.image,
      required this.video});
  factory Post.fromJson(Map<String, dynamic> json) {
    return Post(
        name: json['name'] as String,
        id: json['id'] as int,
        types: json['types'] as String,
        platforms: json['platforms'] as String,
        metaScore: json['metaScore'] as int,
        userScore: json['userScore'] as double,
        image: json['image'] as String,
        video: json['video'] as String);
  }
}
