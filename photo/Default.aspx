<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>::Stadium Events Management System::</title>
        <meta charset="utf-8">
    <meta http-equiv="x-ua-compatible" content="ie=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <link href="css/responsive.css" rel="stylesheet">

    
    <%--<script src="https://use.fontawesome.com/a702fc9918.js"></script>--%>
    
    <script src="js/a702fc9918.js"></script>
   <script type="text/javascript">
var Tawk_API=Tawk_API||{}, Tawk_LoadStart=new Date();
(function(){
var s1=document.createElement("script"),s0=document.getElementsByTagName("script")[0];
s1.async=true;
s1.src='https://embed.tawk.to/57b59cfca5e41d4b0250cbed/default';
s1.charset='UTF-8';
s1.setAttribute('crossorigin','*');
s0.parentNode.insertBefore(s1,s0);
})();
</script>
</head>
<body>

    <form id="form1" runat="server">
    
   
         <div class="modal fade" id="signin" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                  <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                      <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Sign In</h4>
                      </div>
                      <div class="modal-body">

                 <%--    Login with your email to book, pay and manage your stadium entrance access account.--%>
     
                                         <div class="input-group">
    <span class="input-group-addon"><i class="fa fa-edit"></i></span>
                                            <asp:TextBox ID="txtsigninEmail" runat="server" placeholder="Email Address" class="form-control" TextMode="Email"></asp:TextBox>
                                        </div>

                           <br /> <div class="input-group">
    <span class="input-group-addon"><i class="fa fa-edit"></i></span>
                                            <asp:TextBox ID="txtsigninpwd" runat="server" placeholder="Password" class="form-control" TextMode="Password"></asp:TextBox>
                                        </div>
                              <h6> Not Registered yet, kindly  <a href="javascript:;" data-toggle="modal" data-target="#signup"> Sign Up Here</a>
                      </h6>   

                      </div>
                      
                          <div class="modal-footer">
                       
                          <asp:Button ID="btnSignin" class="btn btn-default" runat="server" Text="Login" />
                           <button data-dismiss="modal" class="btn btn-default" type="button">Cancel</button>
                      </div>
                    </div>
                  </div>
                </div>


        <div class="modal fade" id="signup" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                  <div class="modal-dialog">
                    <div class="modal-content">
                      <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Sign Up?</h4>
                      </div>
                      <div class="modal-body">

                       You are about to sign up to start booking for stadium entrance access
                            <br /> <div class="input-group">
    <span class="input-group-addon"><i class="fa fa-user"></i></span>
     <asp:TextBox ID="txtsignupfullName" runat="server" placeholder="Your Full Name" class="form-control"></asp:TextBox>
  </div>
                                      
                                        <br /> <div class="input-group">
    <span class="input-group-addon"><i class="fa fa-edit"></i></span>
                                            <asp:TextBox ID="txtsignupEmail1" runat="server" placeholder="Email Address" class="form-control" TextMode="Email"></asp:TextBox>
                                        </div>

                           <br /> <div class="input-group">
    <span class="input-group-addon"><i class="fa fa-edit"></i></span>
                                            <asp:TextBox ID="txtsignupEmail2" runat="server" placeholder="Repeat Email" class="form-control" TextMode="Email"></asp:TextBox>
                                        </div>
                                 You will receive your login password in the email address herein provided.       
                      </div>
                       
                      <div class="modal-footer">
                       
                          <asp:Button ID="btnSignup" class="btn btn-default" runat="server" Text="Register" />
                           <button data-dismiss="modal" class="btn btn-default" type="button">Cancel</button>
                      </div>
                    </div>
                  </div>
                </div>
     
      <div class="modal fade" id="adminsignin" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                  <div class="modal-dialog">
                    <div class="modal-content">
                      <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Admin Sign In</h4>
                      </div>
                      <div class="modal-body">

                 <%--    Login with your email to book, pay and manage your stadium entrance access account.--%>
     
                                         <div class="input-group">
    <span class="input-group-addon"><i class="fa fa-edit"></i></span>
                                            <asp:TextBox ID="txtAdminuser" runat="server" placeholder="Admin user" class="form-control" TextMode="SingleLine"></asp:TextBox>
                                        </div>

                           <br /> <div class="input-group">
    <span class="input-group-addon"><i class="fa fa-edit"></i></span>
                                            <asp:TextBox ID="txtAdminPwd" runat="server" placeholder="Password" class="form-control" TextMode="Password"></asp:TextBox>
                                        </div>
                             

                      </div>
                      
                          <div class="modal-footer">
                       
                          <asp:Button ID="btnAdmin" class="btn btn-default" runat="server" Text="Login" />
                           <button data-dismiss="modal" class="btn btn-default" type="button">Cancel</button>
                      </div>
                    </div>
                  </div>
                </div>
    <header>

    <div class="header-top">
        <div class="container">
            <div class="row">
                <div class="col-xs-12">
                    <div class="support-info">
                        <p>Questions? Contact Akerele Adebambo  <i class="fa fa-phone-square"></i>+40 740 944 464</p>
                    </div>
                </div>
            </div>
        </div>  <!-- end of container -->
    </div>  <!-- end of header-top -->

      
       
    <div class="nav-wrapper">
        <div class="overlay">

            <nav>
                <div class="navbar">
                    <div class="navbar-header">
                        <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                            <span class="fa fa-bars" style="font-size:30px; color:#fff;"></span>
                        </button>
                        <a class="navbar-brand" href="default.aspx">
                            <h2><i class="fa fa-map-marker"></i> Abuja National Stadium</h2>
                        </a>
                    </div>  <!-- end of navbar-header-->

                    <div class="navbar-collapse collapse">
                        <ul class="nav navbar-nav">
                            <li> <a href="javascript:;" data-toggle="modal" data-target="#signin"><i class="fa fa-bullseye" style="color: #ccff66;"></i>Sign In</a>
                                                      <a href="javascript:;" data-toggle="modal" data-target="#signup"><i class="fa fa-edit" style="color: #ccff66;"></i>Sign Up</a>

                             <a href="javascript:;" data-toggle="modal" data-target="#adminsignin"><i class="fa fa-lock" style="color: #ccff66;"></i> Admin Login</a></li>
                            
                        </ul>
                    </div>   <!-- end of navbar-collapse -->
                </div>  <!-- end of navbar-->
            </nav>

            <div class="caption-text text-center">
                <h3>Stadium Entrance Ticketing System (SETS). <br /></h3>
                <p>DATABASE FOR ONLINE BOOKING & MANAGEMENT OF SALES
               <br />
                    ( A Case Study of M.K.O Abiola Stadium Abuja, Nigeria)
                    <br /><br />
                    By <br />
                    AKERELE ADEBAMBO
                </p>
 <div style="color: black;position: fixed; width: 100%; z-index: 1000; left: 0px; bottom: 0px">
  <a href="javascript:;" data-toggle="modal" data-target="#confirmmodalcalendar"> <h3 "></h3></a>
                </div>
                <p>
                   <a class="btn btn-lg btn-ghost" href="javascript:;" data-toggle="modal" data-target="#confirmmodallogi">Buy New Ticket</a>

                </p>
                
               <!-- <iframe id="Iframe3" frameborder="0" scrolling="no" src="../s/Default.aspx" style="width: 1px;
            height: 1px;">
                </iframe>-->
                
<%--<asp:LinkButton ID="LinkButton1" runat="server" style="position: fixed; width: 100px; z-index: 1000; left: 0px; bottom: 0px;
                        bottom: 0px"> <i class="fa fa-calendar"></i><h4> View Our Event Calendar</h4></asp:LinkButton>
--%>            
            
            </div>

        </div>
    </div>



</header>   <!-- end of header & navigation section-->


<div class="icons-container">
    <div class="container">
        <div class="row text-center">

            <div class="col-xs-12">
                <div class="icon-heading text-center">
                    <h4> </h4>
                </div>
            </div>  <!-- end of col-xs-12 -->

            <div class="icon-list">

                <div class="col-xs-12 col-sm-4">
                    <div class="icon-box">
                       <%-- <img src="images/fuel-pump.png" alt="Fuel Pump Icon">--%>
                      <h1> <i class="fa fa-clock-o" style="color: #ccff66;"></i></h1>
                        <h4>Track activities</h4>
                        <p>Capture all bookings, facility requirements &amp; consumables needs of the event.</p>
                    </div>
                </div>

                <div class="col-xs-12 col-sm-4">
                    <div class="icon-box">
                        <img src="images/analytics.png" alt="Analytics Icon">
                        <h4>Generate Reports</h4>
                        <p>Enjoy accurate data gathering &amp; flexible reports.</p>
                    </div>
                </div>

                <div class="col-xs-12 col-sm-4">
                    <div class="icon-box">
                        <img src="images/padlock.png" alt="Padlock Icon">
                        <h4>Data Security</h4>
                        <p>The entire data of your booked events are securely coded in the database & highly protected. Only
                            persons authorized by you will have access to aspects allowed.
                        </p>
                    </div>
                </div>

            </div>  <!-- end of icon-list -->

        </div>
    </div>
</div>  <!-- end of icon-list -->

   




            <div class="col-sm-4">
                <div class="footer-text shift-right">
                    <h3>Find Us</h3>
                    <p><em><span class="phone-footer fa fa-phone"></span>Master Project By Aderelu Adebambo</em></p>

                    <ul class="footer-social">
                        <li><a href="#"><i class="fa fa-envelope" aria-hidden="true"></i></a></li>
                        <li><a href="#"><i class="fa fa-facebook-square" aria-hidden="true"></i></a></li>
                        <li><a href="#"><i class="fa fa-twitter-square" aria-hidden="true"></i></a></li>
                    </ul>

                </div>
            </div>
    <div>
    
    
    
    
    
      </div>
    
    

       
        
     
     
    <%--   
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="CIN REQUIRED" ControlToValidate="txtCIN"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="USERNAME REQUIRED" ControlToValidate="txtusername"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="PASSWORD REQUIRED" ControlToValidate="txtpassword"></asp:RequiredFieldValidator>--%>

    </form>
</body>
<%--<script src="http://code.jquery.com/jquery-1.11.0.min.js" charset="utf-8"></script>
--%>
<script src="js/jquery-1.11.0.min.js" charset="utf-8"></script>

<script type="text/javascript" src="js/bootstrap.min.js"></script>
<%--<link rel="stylesheet" href="css/templatemo_main.css"/>
--%>

<%--<script type="text/javascript">
    $(window).load(function(){
        $('#confirmmodallogin').modal('show');
    });
</script>--%>
</html>
