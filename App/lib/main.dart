import 'package:flutter/material.dart';

void main() {
  runApp(const ReptiRealmApp());
}

class ReptiRealmApp extends StatelessWidget {
  const ReptiRealmApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('Home')),
        body: const Center(
          child: Text('Hello, world!'),
        ),
      ),
    );
  }
}
