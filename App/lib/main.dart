import 'package:flutter/material.dart';
import 'package:reptirealm/pages/core_pages/home_page.dart';


void main() {
  runApp(const ReptiRealmApp());
}


class ReptiRealmApp extends StatelessWidget {
  const ReptiRealmApp({super.key});

  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      home: HomePage(),
    );
  }
}
