import 'package:flutter/material.dart';
import '../core_pages/widgets/search_bar.dart';
import '../shared/partials/header_bar.dart';
import '../shared/partials/navigation_bar.dart';

class MyReptiles extends StatefulWidget {
  const MyReptiles({super.key});

  @override
  State<MyReptiles> createState() => _MyReptilesState();
}

class _MyReptilesState extends State<MyReptiles> {
  final TextEditingController searchController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(

      appBar: const HeaderBar(),

      body: Column(
        children: [
          MySearchBar(
            controller: searchController,
            text: 'Search reptiles...',
          ),
          const Expanded(
            child: Center(
              child: Text("Page content"),
            ),
          ),
        ],
      ),

      bottomNavigationBar: const NavBar(),
    );
  }
}
