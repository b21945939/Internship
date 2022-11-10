import 'dart:convert';

import 'package:apiuse_flutter/post_model.dart';
import 'package:http/http.dart';

class HttpService {
  final Uri postsUrl = Uri.parse("https://127.0.0.1:44328/api/games");
  Future<List<Post>> getPosts() async {
    Response res = await get(postsUrl);
    if (res.statusCode == 200) {
      List<dynamic> body = jsonDecode(res.body);

      List<Post> posts =
          body.map((dynamic item) => Post.fromJson(item)).toList();
      return posts;
    } else {
      throw res.statusCode.toString();
    }
  }
}
