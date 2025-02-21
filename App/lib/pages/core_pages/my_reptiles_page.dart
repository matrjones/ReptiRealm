import 'package:flutter/material.dart';
import 'package:reptirealm/pages/core_pages/widgets/reptile_card.dart';
import '../../models/reptile.dart';
import '../core_pages/widgets/search_bar.dart';
import '../shared/partials/header_bar.dart';


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

      appBar: const HeaderBar(),

      body: Column(
        children: [
          MySearchBar(
            controller: searchController,
            text: 'Search reptiles...',
          ),
          Expanded(
            child: ListView.builder(
              itemCount: reptiles.length,
              itemBuilder: (context, index) {
                return ReptileCard(reptile: reptiles[index]);
              }
            ),
          ),
        ],
      ),
    );
  }
}
