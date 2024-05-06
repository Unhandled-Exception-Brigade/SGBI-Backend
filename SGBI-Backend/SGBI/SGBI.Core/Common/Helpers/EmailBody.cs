namespace SGBI.SBGI.Core.Common.Helpers;

public static class EmailBody
{
  public static string ChangePasswordRequest(string email, string emailToken)
  {
    var resetLink = $"http://localhost:4200/resetear?email={email}&code={emailToken}";

    return $@"<!DOCTYPE html>
<html lang=""en-US"">
  <head>
    <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"" />
    <title>Reset Password Email</title>
    <meta name=""description"" content=""Reset Password Email"" />
    <style type=""text/css"">
      a:hover {{
        text-decoration: underline !important;
      }}
    </style>
  </head>

  <body
    marginheight=""0""
    topmargin=""0""
    marginwidth=""0""
    style=""margin: 0px; background-color: #f2f3f8""
    leftmargin=""0""
  >
  
    <table
      cellspacing=""0""
      border=""0""
      cellpadding=""0""
      width=""100%""
      bgcolor=""#f2f3f8""
      style=""
        @import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700);
        font-family: 'Open Sans', sans-serif;
      ""
    >
      <tr>
        <td>
          <table
            style=""background-color: #f2f3f8; max-width: 670px; margin: 0 auto""
            width=""100%""
            border=""0""
            align=""center""
            cellpadding=""0""
            cellspacing=""0""
          >
            <tr>
              <td style=""height: 80px"">&nbsp;</td>
            </tr>
            <tr>
              <td style=""text-align: center"">
                <a href="""" title="""" target=""_blank"">
                  <img
                    width=""70""
                    height=""100""
                    src=""https://images.squarespace-cdn.com/content/v1/60ccb6847a9bfe6511ccf9be/6355cb53-5947-443d-a8e5-abd502b88b11/Escudo_Canto%CC%81n_Atenas%2C_Alajuela%2C_Costa_Rica.svg.png?format=750w""
                    title=""""
                    alt=""""
                  />
                </a>
              </td>
            </tr>
            <tr>
              <td style=""height: 20px"">&nbsp;</td>
            </tr>
            <tr>
              <td>
                <table
                  width=""95%""
                  border=""0""
                  align=""center""
                  cellpadding=""0""
                  cellspacing=""0""
                  style=""
                    max-width: 670px;
                    background: #fff;
                    border-radius: 3px;
                    text-align: center;
                    -webkit-box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                    -moz-box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                    box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                  ""
                >
                  <tr>
                    <td style=""height: 40px"">&nbsp;</td>
                  </tr>
                  <tr>
                    <td style=""padding: 0 35px"">
                      <h1
                        style=""
                          color: #1e1e2d;
                          font-weight: 500;
                          margin: 0;
                          font-size: 32px;
                          font-family: 'Rubik', sans-serif;
                        ""
                      >
                        Restablecer la contraseña de su cuenta.
                      </h1>
                      <span
                        style=""
                          display: inline-block;
                          vertical-align: middle;
                          margin: 29px 0 26px;
                          border-bottom: 1px solid #cecece;
                          width: 100px;
                        ""
                      ></span>
                      <p
                        style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 0;
                        ""
                      >
                        Si no realizó esta solicitud, puede ignorar este correo
                        electrónico.
                      </p>
                      <p
                        style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 0;
                        ""
                      >
                        Por favor, tenga en cuenta que este enlace es válido
                        solamente durante los siguientes 15 minutos. Si necesita
                        más ayuda o tiene alguna pregunta, no dude en ponerse en
                        contacto con nuestro equipo de soporte.
                      </p>

                      <a
                        href=""{resetLink}""
                        style=""
                          background: #136137;
                          text-decoration: none !important;
                          font-weight: 500;
                          margin-top: 35px;
                          color: #fff;
                          text-transform: uppercase;
                          font-size: 14px;
                          padding: 10px 24px;
                          display: inline-block;
                          border-radius: 50px;
                        ""
                        >Restablecer Contraseña</a
                      >
                    </td>
                  </tr>
                  <tr>
                    <td style=""height: 40px"">&nbsp;</td>
                  </tr>
                </table>
              </td>
            </tr>

            <tr>
              <td style=""height: 20px"">&nbsp;</td>
            </tr>
            <tr>
              <td style=""text-align: center"">
                <p
                  style=""
                    font-size: 14px;
                    color: rgba(69, 80, 86, 0.7411764705882353);
                    line-height: 18px;
                    margin: 0 0 0;
                  ""
                >
                  &copy; <strong>Sistema de Gestión de Bienes Inmuebles</strong>
                </p>
              </td>
            </tr>
            <tr>
              <td style=""height: 80px"">&nbsp;</td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </body>
</html>
";
  }

    public static string PasswordChangeConfirm(string email)
    {
        return $@"<!DOCTYPE html>
<html lang=""en-US"">
  <head>
    <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"" />
    <title>Reset Password Email</title>
    <meta name=""description"" content=""Reset Password Email"" />
    <style type=""text/css"">
      a:hover {{
        text-decoration: underline !important;
      }}
    </style>
  </head>

  <body
    marginheight=""0""
    topmargin=""0""
    marginwidth=""0""
    style=""margin: 0px; background-color: #f2f3f8""
    leftmargin=""0""
  >
    <!--100% body table-->
    <table
      cellspacing=""0""
      border=""0""
      cellpadding=""0""
      width=""100%""
      bgcolor=""#f2f3f8""
      style=""
        @import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700);
        font-family: 'Open Sans', sans-serif;
      ""
    >
      <tr>
        <td>
          <table
            style=""background-color: #f2f3f8; max-width: 670px; margin: 0 auto""
            width=""100%""
            border=""0""
            align=""center""
            cellpadding=""0""
            cellspacing=""0""
          >
            <tr>
              <td style=""height: 80px"">&nbsp;</td>
            </tr>
            <tr>
              <td style=""text-align: center"">
                <a href="""" title="""" target=""_blank"">
                  <img
                    width=""70""
                    height=""100""
                    src=""https://images.squarespace-cdn.com/content/v1/60ccb6847a9bfe6511ccf9be/6355cb53-5947-443d-a8e5-abd502b88b11/Escudo_Canto%CC%81n_Atenas%2C_Alajuela%2C_Costa_Rica.svg.png?format=750w""
                    title=""""
                    alt=""""
                  />
                </a>
              </td>
            </tr>
            <tr>
              <td style=""height: 20px"">&nbsp;</td>
            </tr>
            <tr>
              <td>
                <table
                  width=""95%""
                  border=""0""
                  align=""center""
                  cellpadding=""0""
                  cellspacing=""0""
                  style=""
                    max-width: 670px;
                    background: #fff;
                    border-radius: 3px;
                    text-align: center;
                    -webkit-box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                    -moz-box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                    box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                  ""
                >
                  <tr>
                    <td style=""height: 40px"">&nbsp;</td>
                  </tr>
                  <tr>
                    <td style=""padding: 0 35px"">
                      <h1
                        style=""
                          color: #1e1e2d;
                          font-weight: 500;
                          margin: 0;
                          font-size: 32px;
                          font-family: 'Rubik', sans-serif;
                        ""
                      >
                        Confirmación de cambio de contraseña
                      </h1>
                      <span
                        style=""
                          display: inline-block;
                          vertical-align: middle;
                          margin: 29px 0 26px;
                          border-bottom: 1px solid #cecece;
                          width: 100px;
                        ""
                      ></span>
                      <p
                        style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 0;
                        ""
                      >
                        Usted ha recibido este correo porque su cambio de
                        contraseña ha sido exitoso.
                      </p>
                      <p
                        style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 0;
                        ""
                      >
                        Si usted no ha cambiado su contraseña, y cree que este
                        correo ha sido un error.
                      </p>
                      <p
                        style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 0;
                        ""
                      >
                        Por favor, contacte nuestro equipo de soporte
                        inmediatamente
                      </p>
                    </td>
                  </tr>
                  <tr>
                    <td style=""height: 40px"">&nbsp;</td>
                  </tr>
                </table>
              </td>
            </tr>

            <tr>
              <td style=""height: 20px"">&nbsp;</td>
            </tr>
            <tr>
              <td style=""text-align: center"">
                <p
                  style=""
                    font-size: 14px;
                    color: rgba(69, 80, 86, 0.7411764705882353);
                    line-height: 18px;
                    margin: 0 0 0;
                  ""
                >
                  &copy; <strong>Sistema de Gestión de Bienes Inmuebles</strong>
                </p>
              </td>
            </tr>
            <tr>
              <td style=""height: 80px"">&nbsp;</td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </body>
</html>
";
    }

    public static string ActivateAccount(string email, string emailToken)
    {
        var resetLink = $"http://localhost:4200/activar?email={email}&code={emailToken}";

        return $@"<!DOCTYPE html>
<html lang=""en-US"">
  <head>
    <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"" />
    <title>Reset Password Email</title>
    <meta name=""description"" content=""Reset Password Email"" />
    <style type=""text/css"">
      a:hover {{
        text-decoration: underline !important;
      }}
    </style>
  </head>

  <body
    marginheight=""0""
    topmargin=""0""
    marginwidth=""0""
    style=""margin: 0px; background-color: #f2f3f8""
    leftmargin=""0""
  >
    <!--100% body table-->
    <table
      cellspacing=""0""
      border=""0""
      cellpadding=""0""
      width=""100%""
      bgcolor=""#f2f3f8""
      style=""
        @import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700);
        font-family: 'Open Sans', sans-serif;
      ""
    >
      <tr>
        <td>
          <table
            style=""background-color: #f2f3f8; max-width: 670px; margin: 0 auto""
            width=""100%""
            border=""0""
            align=""center""
            cellpadding=""0""
            cellspacing=""0""
          >
            <tr>
              <td style=""height: 80px"">&nbsp;</td>
            </tr>
            <tr>
              <td style=""text-align: center"">
                <a href="""" title="""" target=""_blank"">
                  <img
                    width=""70""
                    height=""100""
                    src=""https://images.squarespace-cdn.com/content/v1/60ccb6847a9bfe6511ccf9be/6355cb53-5947-443d-a8e5-abd502b88b11/Escudo_Canto%CC%81n_Atenas%2C_Alajuela%2C_Costa_Rica.svg.png?format=750w""
                    title=""""
                    alt=""""
                  />
                </a>
              </td>
            </tr>
            <tr>
              <td style=""height: 20px"">&nbsp;</td>
            </tr>
            <tr>
              <td>
                <table
                  width=""95%""
                  border=""0""
                  align=""center""
                  cellpadding=""0""
                  cellspacing=""0""
                  style=""
                    max-width: 670px;
                    background: #fff;
                    border-radius: 3px;
                    text-align: center;
                    -webkit-box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                    -moz-box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                    box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                  ""
                >
                  <tr>
                    <td style=""height: 40px"">&nbsp;</td>
                  </tr>
                  <tr>
                    <td style=""padding: 0 35px"">
                      <h1
                        style=""
                          color: #1e1e2d;
                          font-weight: 500;
                          margin: 0;
                          font-size: 32px;
                          font-family: 'Rubik', sans-serif;
                        ""
                      >
                        Activar cuenta de SGBI.
                      </h1>
                      <p
                        style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 24px 0 0 0;
                        ""
                      >
                      Haz sido agregado al sistema SGBI de la Municipalidad de Atenas.
                      </p>
                      <p
                        style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 0;
                        ""
                      >
                      Para proceder con la activación de la cuenta, presiona el botón 'ACTIVAR CUENTA'.
                      </p>
                      <span
                        style=""
                          display: inline-block;
                          vertical-align: middle;
                          margin: 29px 0 26px;
                          border-bottom: 1px solid #cecece;
                          width: 100px;
                        ""
                      ></span>
                      <p
                        style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 0;
                        ""
                      >
                        Si no realizó esta solicitud, puede ignorar este correo
                        electrónico.
                      </p>
                      <p
                        style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 0;
                        ""
                      >
                        Por favor, tenga en cuenta que este enlace es válido
                        solamente durante un tiempo definido. Si necesita
                        más ayuda o tiene alguna pregunta, no dude en ponerse en
                        contacto con nuestro equipo de soporte.
                      </p>

                      <a
                        href=""{resetLink}""
                        style=""
                          background: #136137;
                          text-decoration: none !important;
                          font-weight: 500;
                          margin-top: 35px;
                          color: #fff;
                          text-transform: uppercase;
                          font-size: 14px;
                          padding: 10px 24px;
                          display: inline-block;
                          border-radius: 50px;
                        ""
                        >Activar cuenta</a
                      >
                    </td>
                  </tr>
                  <tr>
                    <td style=""height: 40px"">&nbsp;</td>
                  </tr>
                </table>
              </td>
            </tr>

            <tr>
              <td style=""height: 20px"">&nbsp;</td>
            </tr>
            <tr>
              <td style=""text-align: center"">
                <p
                  style=""
                    font-size: 14px;
                    color: rgba(69, 80, 86, 0.7411764705882353);
                    line-height: 18px;
                    margin: 0 0 0;
                  ""
                >
                  &copy; <strong>Sistema de Gestión de Bienes Inmuebles</strong>
                </p>
              </td>
            </tr>
            <tr>
              <td style=""height: 80px"">&nbsp;</td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </body>
</html>";
    }

    public static string ActivateAccountSucess(string email)
    {
        return $@"<!DOCTYPE html>
<html lang=""en-US"">
  <head>
    <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"" />
    <title>Reset Password Email</title>
    <meta name=""description"" content=""Reset Password Email"" />
    <style type=""text/css"">
      a:hover {{
        text-decoration: underline !important;
      }}
    </style>
  </head>

  <body
    marginheight=""0""
    topmargin=""0""
    marginwidth=""0""
    style=""margin: 0px; background-color: #f2f3f8""
    leftmargin=""0""
  >
    <!--100% body table-->
    <table
      cellspacing=""0""
      border=""0""
      cellpadding=""0""
      width=""100%""
      bgcolor=""#f2f3f8""
      style=""
        @import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700);
        font-family: 'Open Sans', sans-serif;
      ""
    >
      <tr>
        <td>
          <table
            style=""background-color: #f2f3f8; max-width: 670px; margin: 0 auto""
            width=""100%""
            border=""0""
            align=""center""
            cellpadding=""0""
            cellspacing=""0""
          >
            <tr>
              <td style=""height: 80px"">&nbsp;</td>
            </tr>
            <tr>
              <td style=""text-align: center"">
                <a href="""" title="""" target=""_blank"">
                  <img
                    width=""70""
                    height=""100""
                    src=""https://images.squarespace-cdn.com/content/v1/60ccb6847a9bfe6511ccf9be/6355cb53-5947-443d-a8e5-abd502b88b11/Escudo_Canto%CC%81n_Atenas%2C_Alajuela%2C_Costa_Rica.svg.png?format=750w""
                    title=""""
                    alt=""""
                  />
                </a>
              </td>
            </tr>
            <tr>
              <td style=""height: 20px"">&nbsp;</td>
            </tr>
            <tr>
              <td>
                <table
                  width=""95%""
                  border=""0""
                  align=""center""
                  cellpadding=""0""
                  cellspacing=""0""
                  style=""
                    max-width: 670px;
                    background: #fff;
                    border-radius: 3px;
                    text-align: center;
                    -webkit-box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                    -moz-box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                    box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                  ""
                >
                  <tr>
                    <td style=""height: 40px"">&nbsp;</td>
                  </tr>
                  <tr>
                    <td style=""padding: 0 35px"">
                      <h1
                        style=""
                          color: #1e1e2d;
                          font-weight: 500;
                          margin: 0;
                          font-size: 32px;
                          font-family: 'Rubik', sans-serif;
                        ""
                      >
                        Confirmación de Activación de cuenta
                      </h1>
                      <span
                        style=""
                          display: inline-block;
                          vertical-align: middle;
                          margin: 29px 0 26px;
                          border-bottom: 1px solid #cecece;
                          width: 100px;
                        ""
                      ></span>
                      <p
                        style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 0;
                        ""
                      >
                        Usted ha recibido este correo porque su cuenta ha sido
                        activada correctamente.
                      </p>
                      <p
                        style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 0;
                        ""
                      >
                        Si usted no ha activado su cuenta, y cree que este
                        correo ha sido un error.
                      </p>
                      <p
                        style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 0;
                        ""
                      >
                        Por favor, contacte nuestro equipo de soporte
                        inmediatamente
                      </p>
                    </td>
                  </tr>
                  <tr>
                    <td style=""height: 40px"">&nbsp;</td>
                  </tr>
                </table>
              </td>
            </tr>

            <tr>
              <td style=""height: 20px"">&nbsp;</td>
            </tr>
            <tr>
              <td style=""text-align: center"">
                <p
                  style=""
                    font-size: 14px;
                    color: rgba(69, 80, 86, 0.7411764705882353);
                    line-height: 18px;
                    margin: 0 0 0;
                  ""
                >
                  &copy; <strong>Sistema de Gestión de Bienes Inmuebles</strong>
                </p>
              </td>
            </tr>
            <tr>
              <td style=""height: 80px"">&nbsp;</td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </body>
</html>
";
    }
    
    public static string ChangeEmailSucess(string email)
    {
      return $@"<!DOCTYPE html>
<html lang=""en-US"">
  <head>
    <meta content=""text/html; charset=utf-8"" http-equiv=""Content-Type"" />
    <title>Reset Password Email</title>
    <meta name=""description"" content=""Reset Password Email"" />
    <style type=""text/css"">
      a:hover {{
        text-decoration: underline !important;
      }}
    </style>
  </head>

  <body
    marginheight=""0""
    topmargin=""0""
    marginwidth=""0""
    style=""margin: 0px; background-color: #f2f3f8""
    leftmargin=""0""
  >
    <!--100% body table-->
    <table
      cellspacing=""0""
      border=""0""
      cellpadding=""0""
      width=""100%""
      bgcolor=""#f2f3f8""
      style=""
        @import url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700);
        font-family: 'Open Sans', sans-serif;
      ""
    >
      <tr>
        <td>
          <table
            style=""background-color: #f2f3f8; max-width: 670px; margin: 0 auto""
            width=""100%""
            border=""0""
            align=""center""
            cellpadding=""0""
            cellspacing=""0""
          >
            <tr>
              <td style=""height: 80px"">&nbsp;</td>
            </tr>
            <tr>
              <td style=""text-align: center"">
                <a href="""" title="""" target=""_blank"">
                  <img
                    width=""70""
                    height=""100""
                    src=""https://images.squarespace-cdn.com/content/v1/60ccb6847a9bfe6511ccf9be/6355cb53-5947-443d-a8e5-abd502b88b11/Escudo_Canto%CC%81n_Atenas%2C_Alajuela%2C_Costa_Rica.svg.png?format=750w""
                    title=""""
                    alt=""""
                  />
                </a>
              </td>
            </tr>
            <tr>
              <td style=""height: 20px"">&nbsp;</td>
            </tr>
            <tr>
              <td>
                <table
                  width=""95%""
                  border=""0""
                  align=""center""
                  cellpadding=""0""
                  cellspacing=""0""
                  style=""
                    max-width: 670px;
                    background: #fff;
                    border-radius: 3px;
                    text-align: center;
                    -webkit-box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                    -moz-box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                    box-shadow: 0 6px 18px 0 rgba(0, 0, 0, 0.06);
                  ""
                >
                  <tr>
                    <td style=""height: 40px"">&nbsp;</td>
                  </tr>
                  <tr>
                    <td style=""padding: 0 35px"">
                      <h1
                        style=""
                          color: #1e1e2d;
                          font-weight: 500;
                          margin: 0;
                          font-size: 32px;
                          font-family: 'Rubik', sans-serif;
                        ""
                      >
                        Cambio de Correo Electrónico
                      </h1>
                      <span
                        style=""
                          display: inline-block;
                          vertical-align: middle;
                          margin: 29px 0 26px;
                          border-bottom: 1px solid #cecece;
                          width: 100px;
                        ""
                      ></span>
                      <p
                        style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 0;
                        ""
                      >
                        Usted ha recibido este correo porque su correo electrónico ha sido actualizado a esta dirección.
                      </p>
                      <p
                        style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 0;
                        ""
                      >
                        Si usted considera que este correo ha sido un error.
                      </p>
                      <p
                        style=""
                          color: #455056;
                          font-size: 15px;
                          line-height: 24px;
                          margin: 0;
                        ""
                      >
                        Por favor, contacte nuestro equipo de soporte
                        inmediatamente
                      </p>
                    </td>
                  </tr>
                  <tr>
                    <td style=""height: 40px"">&nbsp;</td>
                  </tr>
                </table>
              </td>
            </tr>

            <tr>
              <td style=""height: 20px"">&nbsp;</td>
            </tr>
            <tr>
              <td style=""text-align: center"">
                <p
                  style=""
                    font-size: 14px;
                    color: rgba(69, 80, 86, 0.7411764705882353);
                    line-height: 18px;
                    margin: 0 0 0;
                  ""
                >
                  &copy; <strong>Sistema de Gestión de Bienes Inmuebles</strong>
                </p>
              </td>
            </tr>
            <tr>
              <td style=""height: 80px"">&nbsp;</td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </body>
</html>";
    }
}