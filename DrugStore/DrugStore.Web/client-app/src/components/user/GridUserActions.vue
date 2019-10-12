<template>
  <v-container>
    <v-layout>
      <v-card flat color="white">
        <v-card-title>Usuarios
          <v-spacer></v-spacer>
          <v-text-field
            v-model="search"
            append-icon="search"
            label="Search"
            single-line              
          ></v-text-field>
          <v-spacer></v-spacer>          

          <!-- Dialog for adding new product -->
          <v-dialog v-model="dialogNewUser" persistent max-width="600px">
            <template v-slot:activator="{ on }">
              <v-btn color="primary" dark class="mb-2" v-on="on">Nuevo Usuario</v-btn>
            </template>
              <FormNewUser    
                :roles="roles"                          
                @save="addNewUser"
                @cancel="closeDialog"
              />       
          </v-dialog>         
        </v-card-title> 
        
        <v-data-table        
          :headers="headersDataGrid"
          :items="users"
          :search="search"
          class="elevation-1"      
        >                                                          
          
          <template v-slot:item.condition="{item}">                
              <div v-if="item.condition">
                <span class="blue--text">Activo</span>
              </div>
              <div v-else>
                <span class="red--text">Inactivo</span>
              </div>
          </template>

          <template v-slot:item.action="{ item }">         
            <v-icon
              small
              class="mr-2"                  
              @click="showUserForEdit(item)"
            >
              edit
            </v-icon>                                                                
            <template v-if="item.condition">
              <v-icon
                small
                @click="activateDeactivateShowUser(2,item)"
              >
                block
              </v-icon>
            </template>
             <template v-else>
              <v-icon
                small
                @click="activateDeactivateShowUser(1,item)"
              >
                check
              </v-icon>
            </template>
          </template>  
        </v-data-table>
      </v-card>

      <template>            
        <v-dialog v-model="dialogEdit" max-width="600px">       
          <FormEditUser
            :user="user"  
            :roles="roles"                                    
            @save="updateUser"
            @cancel="closeDialog"
          />  
                      
        </v-dialog>                              
      </template>

      <template>
        <v-dialog 
          v-model="adModal"
          max-width="290"
          >
            <v-card>
              <v-card-title class="headline" v-if="adAction === 1">¿Activar Usuario?</v-card-title>
              <v-card-title class="headline" v-if="adAction === 2">¿Desactivar Usuario?</v-card-title>
              <v-card-text>
                Estas a punto de
                <span v-if="adAction === 1">Activar</span>
                <span v-if="adAction === 2">Desactivar</span>
                el usuario {{ adName}}
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="green darken-1" flat="flat" @click="adModal=0">Cancelar</v-btn>
                <v-btn v-if="adAction== 1" color="green darken-4" @click="activate()">Aceptar</v-btn>
                <v-btn v-if="adAction== 2" color="green darken-4" @click="deactivate()">Aceptar</v-btn>
              </v-card-actions>
            </v-card>
        </v-dialog>
      </template>

    </v-layout>
  </v-container>
</template>

<script> 
  import axios from 'axios'   
  import FormNewUser from '@/components/user/FormNewUser'
  import FormEditUser from '@/components/user/FormEditUser'

  export default { 
    components: {
      FormNewUser,
      FormEditUser
    },
    data() {
      return {
        headersDataGrid:[
          {text:'ID', value:'idUser'},
          {text:'Rol', value:'role'},
          {text:'Nombre de usuario', value:'userName'},         
          {text:'Numero De Documento', value:'documentNumber'},
          {text:'Direccion', value:'address'},
          {text:'Numero de telefono', value:'phoneNumber'},
          {text:'Email', value:'email'},
          {text:'Estado', value:'condition'},
          { text: 'Actions', value: 'action', sortable: false }
        ],
        users:[],
        roles:[],
        user:'',        
        search:'',        
        dialogNewUser:false,
        dialogEdit:false,
        adModal:0,  //activar o desactivar el modal      
        adAction:0,
        adName:'',
        adId:''
      }
    },
    created () {
      this.listUsers() 
      this.fetchRoles()    
    },   
    methods: {
      listUsers() {
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.get('api/user/list',headers)
        .then(function (response) {
          // handle success
          me.users = response.data                                                    
          })
        .catch(function (error) {
          // handle error          
          console.log(error);   
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }      
        })
      },    
      fetchRoles(){
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.get('api/role/list',headers)
        .then(function (response) {
          // handle success
          me.roles = response.data   
          // console.log(response.data )                                               
          })
        .catch(function (error) {
          // handle error          
          console.log(error); 
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }        
        })
      },        
      addNewUser(user){   
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.post('api/user/create/',
          {            
            'IdRole':user.idRole,
            'UserName':user.userName,
            'DocumentType':user.documentType,            
            'DocumentNumber':user.documentNumber,
            'Address':user.address,
            'PhoneNumber':user.phoneNumber,
            'Email':user.email,
            'Password':user.password,
            'State':user.condition
          }, headers)
        .then(function (response) {
          // handle success
          console.log('success') 
          me.listUsers()
          me.closeDialog()                                                    
          })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      //get by id
      showUserForEdit(item){                    
        this.user = item       
        console.log(item)
        this.dialogEdit = true      
      },
      updateUser(item){
        this.user = item.user
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}              
        axios.put('api/user/update',
        {
          'IdUser':me.user.idUser,
          'IdRole':me.user.idRole,
          'UserName':me.user.userName,      
          'DocumentType':me.user.documentType,            
          'DocumentNumber':me.user.documentNumber,
          'Address':me.user.address,
          'PhoneNumber':me.user.phoneNumber,
          'Email':me.user.email,
          'Password':'',
          'Act_Password':false
        },headers)
        .then(function (response) {
          // handle success
          console.log('Update Successful')       
          me.closeDialog()     
          me.listUsers()                                                                      
        })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      activateDeactivateShowUser(action, item){
        this.adModal = 1 // se muestra el modal de confirmacion
        this.adName = item.userName
        this.adId =  item.idUser      

        if(action === 1)
        {
          this.adAction = 1
        }
        else if(action === 2)
        {
           this.adAction = 2
        }
        else{
          this.adModal = 0
        }
      },
      activate(){
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.put('api/user/Activate/'+ this.adId, {}, headers)
        .then(function (response) {
          // handle success     
          me.adModal = 0
          me.adAction = 0
          me.adName = ""
          me.adId = ""
          me.listUsers()                                                                  
          })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      deactivate(){
        let me=this;                    
        let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
        let headers = {headers:AuthorizationHeader}
        axios.put('api/user/Deactivate/'+ this.adId, {}, headers)
        .then(function (response) {
          // handle success         
          me.adModal = 0
          me.adAction = 0
          me.adName = ""
          me.adId = ""
          me.listUsers()                                                                 
          })
        .catch(function (error) {
          // handle error          
          console.log(error);  
          if (error.response.status === 401) {						
						me.$store.dispatch('exit')
          }       
        })
      },
      clearForm(){

      },
      closeDialog(switchBool){
        this.dialogEdit = switchBool
        this.dialogNewUser = switchBool
      }      
    },
  }
</script>
