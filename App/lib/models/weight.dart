class Weight {
  final String? id;
  final DateTime date;
  final double value;
  final String unit;
  final String? comment;

  Weight({
    this.id,
    required this.date,
    required this.value,
    required this.unit,
    this.comment,
  });
}
