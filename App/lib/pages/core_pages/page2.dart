import 'package:flutter/material.dart';
import 'package:reptirealm/pages/shared/partials/header_bar.dart';
import 'package:reptirealm/pages/shared/partials/navigation_bar.dart';


class Page2 extends StatefulWidget {
  const Page2({super.key});

  @override
  State<Page2> createState() => _Page2State();
}


class _Page2State extends State<Page2> {
  final TextEditingController searchController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return const Scaffold(

      appBar: HeaderBar(),

      body: Center(
        child: Text(
          "Page 2 content",
          style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
        ),
      ),

      bottomNavigationBar: NavBar(currentIndex: 1),
    );
  }
}
