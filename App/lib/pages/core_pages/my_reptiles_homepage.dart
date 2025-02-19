import 'package:flutter/material.dart';
import 'package:reptirealm/pages/shared/partials/header_bar.dart';

class MyReptiles extends StatefulWidget {
  const MyReptiles ({super.key});

  @override
  State<MyReptiles> createState() => _HomepageState();
}


class _HomepageState extends State<MyReptiles> {
  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      home: Scaffold(
        appBar: HeaderBar(),
        body: Center(
          child: Text('Individual reptile cards go here'),
        ),
      ),
    );
  }
}
