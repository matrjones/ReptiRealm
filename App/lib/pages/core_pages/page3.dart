import 'package:flutter/material.dart';
import 'package:reptirealm/pages/shared/partials/header_bar.dart';
import 'package:reptirealm/pages/shared/partials/navigation_bar.dart';


class Page3 extends StatefulWidget {
  const Page3({super.key});

  @override
  State<Page3> createState() => _Page3State();
}


class _Page3State extends State<Page3> {
  final TextEditingController searchController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return const Scaffold(

      appBar: HeaderBar(),

      body: Center(
        child: Text(
          "Page 3 content",
          style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
        ),
      ),

      bottomNavigationBar: NavBar(currentIndex: 2),
    );
  }
}
