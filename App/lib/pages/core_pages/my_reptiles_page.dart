import 'package:flutter/material.dart';
import 'package:reptirealm/models/reptile.dart';
import 'package:reptirealm/pages/core_pages/widgets/add_new_reptile_card.dart';
import 'package:reptirealm/pages/core_pages/widgets/reptile_card.dart';
import 'package:reptirealm/pages/core_pages/widgets/search_bar.dart';
import 'package:reptirealm/pages/shared/partials/header_bar.dart';
import 'package:reptirealm/pages/shared/partials/navigation_bar.dart';


class MyReptiles extends StatefulWidget {
  const MyReptiles({super.key});

  @override
  State<MyReptiles> createState() => _MyReptilesState();
}


class _MyReptilesState extends State<MyReptiles> {
  final TextEditingController searchController = TextEditingController();

  // MOCK REPTILE DATA
  final List<Reptile> reptiles = [
    Reptile(name: "Ra"),
    Reptile(name: "Anubis"),
    Reptile(name: "Lucifer"),
  ];

  // SEARCH BAR IMPLEMENTATION
  List<Reptile> filteredReptiles = [];

  @override
  void initState() {
    super.initState();
    filteredReptiles = reptiles;

    // Listen for search text changes
    searchController.addListener(() {
      final query = searchController.text.toLowerCase();
      setState(() {
        filteredReptiles = reptiles
            .where((r) => r.name.toLowerCase().startsWith(query))
            .toList();
      });
    });
  }

  @override
  void dispose() {
    searchController.dispose();
    super.dispose();
  }

  // PAGE BUILD
  @override
  Widget build(BuildContext context) {
    return Scaffold(

      // PUT HEADER AT TOP OF APP
      appBar: const HeaderBar(),

      body: Column(
        children: [

          // SEARCH BAR
          MySearchBar(
            controller: searchController,
            text: 'Search Reptiles...',
          ),

          // HOME SCREEN BUTTONS
          Expanded(
            child: GridView.builder(
              padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
              itemCount: filteredReptiles.length + 1,
              gridDelegate: const SliverGridDelegateWithMaxCrossAxisExtent(
                maxCrossAxisExtent: 250,
                mainAxisSpacing: 1,
                crossAxisSpacing: 1,
                childAspectRatio: 4 / 2, // Adjust based on how tall you want the cards
              ),
              itemBuilder: (context, index) {
                if (index < filteredReptiles.length) {
                  return ReptileCard(reptile: filteredReptiles[index]);
                } else {
                  return const AddNewReptileCard();
                }
              },
            ),
          ),
        ],
      ),

      // PUT NAV BAR AT BOTTOM OF APP
      bottomNavigationBar: const NavBar(currentIndex: 0,),
    );
  }
}
