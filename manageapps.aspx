<%@ Page Language="vb" AutoEventWireup="false" Debug ="true"  CodeFile="manageapps.aspx.vb" Inherits="manageapps" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
  <meta charset="utf-8">
  <meta content="width=device-width, initial-scale=1.0" name="viewport">

  <title>S3 Home</title>
  <meta content="" name="description">
  <meta content="" name="keywords">

  <!-- Favicons -->
  <link href="myimage/logo.png" rel="icon">
<%--  <link href="assets/img/apple-touch-icon.png" rel="apple-touch-icon">--%>

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
           <script type="text/javascript">  function preventBack() {window.history.forward();}  setTimeout("preventBack()", 0);  window.onunload = function () {null};</script>

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
        <h1><a href="aboutintranet.aspx">Halogen S3</a></h1>
        <!-- Uncomment below if you prefer to use an image logo -->
        <!-- <a href="index.html"><img src="assets/img/logo.png" alt="" class="img-fluid"></a>-->
      </div>

      <nav id="navbar" class="navbar">
        <ul>
          <li><a class="active" href="aboutintranet.aspx">Home</a></li><li><a href="changepassword.aspx">Change Password</a></li>
         <li class="dropdown"><a href="#"><span>Tools</span> <i class="bi bi-chevron-down"></i></a>
            <ul>
              <li><a href="manageapps.aspx">Add/Remove Software</a></li>
               <li class="dropdown"><a href="#"><span>MFA Setting</span> <i class="bi bi-chevron-right"></i></a>
                <ul>
                 
                    <li><a href="managesettings.aspx">Email & MFA Validations </a></li><li><a href="AuthenticationHistory.aspx">MFA Validations History </a></li>
                    <li><a href="ActivityHistory.aspx">Activities History</a></li><li><a href="manageroles.aspx">User Roles Management</a></li>
                  
                  
                </ul>
              </li>

              <li class="dropdown"><a href="#"><span>Add /Manage</span> <i class="bi bi-chevron-right"></i></a>
                <ul>
                  <li><a href="manageusers.aspx">Users Register</a></li>
                    <li><a href="managedept.aspx">Departments</a></li>
                  <li><a href="managebranch.aspx">Branch Offices</a></li>
                  <li><a href="manageregion.aspx">Regional Setup</a></li>
                  
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
  </header><!-- End Header -->
    <form runat ="server" > 
  <main id="main">

    <!-- ======= Featured Section ======= -->
    <section id="featured" class="featured">
      <div class="container">
          <hr />
            <div class="row">
          <div class="col-lg-12">
          
            <ol class="breadcrumb">
              <li><i class="bi bi-home"></i><a href="aboutintranet.aspx">Home  </a></li>
              <li><i class="fa fa-bars"></i>  - Application Register</li>
            
            </ol>
          </div>
        </div>
<ol class="breadcrumbs">
    <div class="col-lg-12" style ="height :100%">
                   
         <div class="panel panel-default" style ="height :100%" >
                       <%-- <div align="center" class="panel-heading" style="background-color:white; color:gray; font-size:smaller ">
                           
                        </div>--%>
                        <div class="panel-body" style ="height :100%" >
                            <div class="row" style ="height :100%" >
                                <div class="col-lg-4" style ="height :100%" >
                               <%-- <div class="form-group">--%>
   
       <div class="input-group">
    <span class="input-group-addon"><i class="fa fa-home"></i></span>
                                            <asp:TextBox ID="txtApplicationName" runat="server" placeholder="Application Name" class="form-control input-sm"></asp:TextBox>
                                        </div>
                                        
                                        <br /> <div class="input-group">
    <span class="input-group-addon"><i class="fa fa-envelope-o"></i></span>
                                            <asp:TextBox ID="txtAppDescription" runat="server" placeholder="App Description" class="form-control input-sm"></asp:TextBox>
                                        </div><br />
                                                                              <div class="form-group">
                                            <asp:Button ID="btnRegister" class="btn btn-primary btn-sm" runat="server" Text="Register" />
                                        <asp:Button ID="BtnClear" class="btn btn-info btn-sm" runat="server" Text="Clear" />&nbsp;
                                            <asp:Button ID="btnUpdate" class="btn btn-success btn-sm" runat="server" Text="Update" Visible="False" />&nbsp;
                                                   <asp:Button ID="BtnDelete" class="btn btn-danger btn-sm" runat="server" Text="Delete" Visible="False" />&nbsp;

                                        </div>
                                </div>
                                <!-- /.col-lg-6 (nested) --><div class="col-lg-8">
                                   
                                        <h4 >
                                       
                                        <asp:GridView ID="GridUser" class="table table-striped table-bordered table-hover" runat="server" Font-Size="Small" AutoGenerateSelectButton="false" Font-Bold="True">
                                           <Columns>
                                       <asp:CommandField ButtonType="link" SelectText='<i class="fa fa-edit"></i>Edit' ShowHeader="True" ShowSelectButton="True" ItemStyle-Font-Bold="" ItemStyle-Font-Size="8" ItemStyle-ForeColor="midnightblue" />

</Columns>
                                            </asp:GridView>
                                        
                                        
                                        </h4>
                                   
                                        
                                    
                                        
                                    
                                </div>
                                <!-- /.col-lg-6 (nested) -->
                            </div>
                            <!-- /.row (nested) --></div>
                        <!-- /.panel-body --></div>
                    <!-- /.panel --></div>
    </ol>
    
            
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

</body>

</html>