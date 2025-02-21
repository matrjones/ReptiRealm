import 'package:flutter/material.dart';
import 'package:reptirealm/pages/core_pages/widgets/add_new_reptile_card.dart';
import 'package:reptirealm/pages/core_pages/widgets/reptile_card.dart';
import '../../models/reptile.dart';
import '../core_pages/widgets/search_bar.dart';


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

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
        children: [

          // SEARCH BAR
          MySearchBar(
            controller: searchController,
            text: 'Search reptiles...',
          ),

          // INDIVIDUAL REPTILE BUTTONS
          Expanded(
            child: ListView.builder(
              itemCount: reptiles.length + 1,
              itemBuilder: (context, index) {
                if(index < reptiles.length) {
                  return ReptileCard(reptile: reptiles[index]);
                }
                else {
                  return const AddNewReptileCard();
                }
              }
            ),
          ),
        ],
      ),
    );
  }
}
