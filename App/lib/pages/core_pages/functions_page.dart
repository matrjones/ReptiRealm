import 'package:flutter/material.dart';
import 'package:reptirealm/pages/core_pages/widgets/add_defecation_card.dart';
import 'package:reptirealm/pages/core_pages/widgets/add_feed_card.dart';
import 'package:reptirealm/pages/core_pages/widgets/add_regurge_card.dart';
import 'package:reptirealm/pages/core_pages/widgets/add_shed_card.dart';
import 'package:reptirealm/pages/core_pages/widgets/add_weight_card.dart';
import 'package:reptirealm/pages/core_pages/widgets/edit_reptile_card.dart';
import 'package:reptirealm/pages/core_pages/widgets/view_defecation_card.dart';
import 'package:reptirealm/pages/core_pages/widgets/view_feed_card.dart';
import 'package:reptirealm/pages/core_pages/widgets/view_shed_card.dart';
import 'package:reptirealm/pages/core_pages/widgets/view_weight_card.dart';
import 'package:reptirealm/pages/shared/partials/header_bar.dart';
import 'package:reptirealm/pages/shared/partials/navigation_bar.dart';


class ReptileFunctions extends StatefulWidget {
  const ReptileFunctions({super.key});

  @override
  State<ReptileFunctions> createState() => _FunctionsState();
}

class _FunctionsState extends State<ReptileFunctions> {
  @override
  Widget build(BuildContext context) {
    return const Scaffold(

      appBar: HeaderBar(),

      body: Padding(
        padding: EdgeInsets.all(16.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [

            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Expanded(child: AddFeedCard()),
                SizedBox(width: 16), // Space between cards
                Expanded(child: ViewFeedCard()),
              ],
            ),
            SizedBox(height: 8), // Space between rows

            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Expanded(child: AddShedCard()),
                SizedBox(width: 16), // Space between cards
                Expanded(child: ViewShedCard()),
              ],
            ),
            SizedBox(height: 8), // Space between rows

            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Expanded(child: AddDefecationCard()),
                SizedBox(width: 16), // Space between cards
                Expanded(child: ViewDefecationCard()),
              ],
            ),
            SizedBox(height: 8), // Space between rows

            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Expanded(child: AddWeightCard()),
                SizedBox(width: 16), // Space between cards
                Expanded(child: ViewWeightCard()),
              ],
            ),
            SizedBox(height: 8), // Space between rows

            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                Expanded(child: AddRegurgeCard()),
                SizedBox(width: 16), // Space between cards
                Expanded(child: EditReptileCard()),
              ],
            ),
          ],
        ),
      ),

      bottomNavigationBar: NavBar(),
    );
  }
}
