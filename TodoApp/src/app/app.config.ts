//const { CLIENT_ID, ISSUER, OKTA_TESTING_DISABLEHTTPSCHECK } = process.env;
//const USE_CLASSIC_ENGINE = process.env.USE_CLASSIC_ENGINE || false;

export default {
  oidc: {
    clientId: '0oa9jsz5b8rk9zdXI5d7',
    issuer: 'https://dev-46138610.okta.com',
    redirectUri: 'http://localhost:4200/login/callback',
    scopes: ['openid', 'profile', 'email'],
    testing: {
      disableHttpsCheck: '${OKTA_TESTING_DISABLEHTTPSCHECK}'
    },
  },
  widget: {
    USE_CLASSIC_ENGINE: '${USE_CLASSIC_ENGINE}',
  },
  resourceServer: {
    messagesUrl: 'http://localhost:4200/api/messages',
  },
};
