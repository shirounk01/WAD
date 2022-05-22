$("#flight").click(function () {
    var x = document.getElementById("searchHotel");
    var y = document.getElementById("searchFlight");

    x.style.display = "none";
    y.style.display = "block";
});

$("#hotel").click(function () {
    var x = document.getElementById("searchFlight");
    var y = document.getElementById("searchHotel");

    x.style.display = "none";
    y.style.display = "block";
});

$("#showMoreReviews").click(function () {
    var x = document.getElementById("showMoreReviews");
    var y = document.getElementById("reviewSection");

    x.style.display = "none";
    y.style.display = "block";
});
document.getElementById("departing").min = new Date().toJSON().slice(0, 10).replace(/-/g, '-');
document.getElementById("returning").min = new Date().toJSON().slice(0, 10).replace(/-/g, '-');
document.getElementById("checkin").min = new Date().toJSON().slice(0, 10).replace(/-/g, '-');
document.getElementById("checkout").min = new Date().toJSON().slice(0, 10).replace(/-/g, '-');

jQuery(document).ready(function () {
    $("#departing").change(function () {
        document.getElementById("returning").min = document.getElementById("departing").value;
        document.getElementById("returning").value = document.getElementById("departing").value;
    });
    $("#checkin").change(function () {
        document.getElementById("checkout").min = document.getElementById("checkin").value;
        document.getElementById("checkout").value = document.getElementById("checkin").value;
    });
});
