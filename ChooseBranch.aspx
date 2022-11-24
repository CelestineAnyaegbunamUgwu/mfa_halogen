﻿<%@ Page Language="vb" AutoEventWireup="false" Debug ="true"  CodeFile="ChooseBranch.aspx.vb" Inherits="ChooseBranch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
  <meta charset="utf-8">
  <meta content="width=device-width, initial-scale=1.0" name="viewport">

  <title>Choose Branch</title>
  <meta content="" name="description">
  <meta content="" name="keywords">

  <!-- Favicons -->
<link href="myimage/logo.png" rel="icon">
<%--      <link href="assets/img/apple-touch-icon.png" rel="apple-touch-icon">--%>

  <!-- Google Fonts -->
  <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">

  <!-- Vendor CSS Files -->
  <link href="assets/vendor/animate.css/animate.min.css" rel="stylesheet">
  <link href="assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
  <link href="assets/vendor/bootstrap-icons/bootstrap-icons.css" rel="stylesheet">
  <link href="assets/vendor/boxicons/css/boxicons.min.css" rel="stylesheet">
  <link href="assets/vendor/glightbox/css/glightbox.min.css" rel="stylesheet">
  <link href="assets/vendor/swiper/swiper-bundle.min.css" rel="stylesheet">

  <!-- Template Main CSS File -->
  <link href="assets/css/style.css" rel="stylesheet">

</head>

<body>
    
  <!-- ======= Top Bar ======= -->
  <section id="topbar" class="d-flex align-items-center">
    <div class="container d-flex justify-content-center justify-content-md-between">
      <div class="contact-info d-flex align-items-center">
        <i class="bi bi-envelope d-flex align-items-center"><a href="mailto:john5cele@yahoo.com">john5cele@yahoo.com</a></i>
        <i class="bi bi-phone d-flex align-items-center ms-4"><span>+234 8184 2536 22</span></i>
      </div>
      <div class="social-links d-none d-md-flex align-items-center">
        <a href="#" class="twitter"><i class="bi bi-twitter"></i></a>
        <a href="#" class="facebook"><i class="bi bi-facebook"></i></a>
        <a href="#" class="instagram"><i class="bi bi-instagram"></i></a>
        <a href="#" class="linkedin"><i class="bi bi-linkedin"></i></i></a>
      </div>
    </div>
  </section>

  <!-- ======= Header ======= -->
  <header id="header" class="d-flex align-items-center">
    <div class="container d-flex justify-content-between align-items-center">

      <div class="logo">
        <h1><a href="Default.aspx">Halogen S3</a></h1>
        <!-- Uncomment below if you prefer to use an image logo -->
        <!-- <a href="index.html"><img src="assets/img/logo.png" alt="" class="img-fluid"></a>-->
      </div>

      <nav id="navbar" class="navbar">
        <ul>
          <li><a class="active" href="Default.aspx">Home</a></li>
         <li class="dropdown"><a href="#"><span>About</span> <i class="bi bi-chevron-down"></i></a>
            <ul>
              <li><a href="#">MFA</a></li>
              <li><a href="#">S3</a></li>
              <li class="dropdown"><a href="#"><span>About Partners</span> <i class="bi bi-chevron-right"></i></a>
                <ul>
                  <li><a href="#">Halogen Group</a></li>
                  <li><a href="#">Ministry of Youths</a></li>
                  <li><a href="#">Celestine Ugwu</a></li>
                  
                </ul>
              </li>
             
             
            </ul>
          </li>
          <li>
            <a href ="" data-bs-toggle="modal" data-bs-target="#logout">Logout</a>

          </li>
         
          
        </ul>
        <i class="bi bi-list mobile-nav-toggle"></i>
      </nav><!-- .navbar -->

    </div>
  </header><!-- End Header -->
    <form runat ="server" > 
  <main id="main">

    <!-- ======= Featured Section ======= -->
    <section id="featured" class="featured">
      <div class="container">

        
         <div class="col-md-12">     
  <div class="row">
        
        
          <div class="col-lg-12">
              <hr />
              
         
              
              <h4 class="breadcrumb"> Your Authorized Regional Branch Offices

 </h4> 
               <p class="description">
                 There could be other branches available, however, you can only see those you are athorized to access.

              </p>
              <h6>
                  
                  <asp:GridView ID="Gridregion" runat="server" Class="table table-striped table-bordered breadcrumb" ShowHeader="True" GridLines="Horizontal" AutoGenerateSelectButton="True"></asp:GridView>
              </h6>
              
             
            </div>
         
          </div>
      
      
       
        
 
        </div>

      </div>
    </section><!-- End Featured Section -->

    <!-- ======= About Section ======= -->
    <!-- End About Section -->

  </main><!-- End #main -->
</form>
  <!-- ======= Footer ======= -->
  <footer id="footer">

    

    

    <div class="container">
      <div class="copyright">
        &copy; Project Submitted to <strong><span>Halogen Group & Federal Ministry of Youths and Sports</span></strong>.
      </div>
      <div class="credits">
       
        Designed by <a href="">Ugwu,Celestine Anyaegbunam</a>
      </div>
    </div>
  </footer><!-- End Footer -->

  <a href="#" class="back-to-top d-flex align-items-center justify-content-center"><i class="bi bi-arrow-up-short"></i></a>

  <!-- Vendor JS Files -->
  <script src="assets/vendor/purecounter/purecounter_vanilla.js"></script>
  <script src="assets/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
  <script src="assets/vendor/glightbox/js/glightbox.min.js"></script>
  <script src="assets/vendor/isotope-layout/isotope.pkgd.min.js"></script>
  <script src="assets/vendor/swiper/swiper-bundle.min.js"></script>
  <script src="assets/vendor/waypoints/noframework.waypoints.js"></script>
  <script src="assets/vendor/php-email-form/validate.js"></script>

  <!-- Template Main JS File -->
  <script src="assets/js/main.js"></script>

    <div class="modal fade" id="logout" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Logout</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        Are you sure you want to logout?
      </div>
      <div class="modal-footer">
           
           <a class="btn btn-primary" href="default.aspx"> Yes </a>
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">No</button>
       
      </div>
    </div>
  </div>
</div>


    
    
</body>

</html>