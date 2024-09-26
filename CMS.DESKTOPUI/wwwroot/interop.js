window.printTable = function (tableClass, title) {
    debugger;
    var tableToPrint = document.querySelector('.' + tableClass);
    if (tableToPrint) {
        // Create a new window or use an iframe for printing
        var printWindow = window.open('', '_blank', 'width=800,height=600');
        printWindow.document.write('<html><head><title>' + title + '</title>');
        printWindow.document.write('<link rel="stylesheet" href="/css/printTable.css">');
        printWindow.document.write('</head><body>');
        printWindow.document.write(tableToPrint.outerHTML);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        printWindow.print();
    } else {
        console.error('Table not found with class: ' + tableClass);
    }
};