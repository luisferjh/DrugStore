<template>
  <v-container>
    <v-layout>
       <v-card>
    <v-card-title>
      <span class="headline">Cambiar Contraseña</span>
    </v-card-title>
    <v-card-text>
      <v-form>      
        <v-container>
          <v-row>                        
            <v-col cols="12" md="12">
              <v-text-field 
                label="Contraseña Anterior" 
                v-model="oldPassword"
                :append-icon="show1 ? 'visibility' : 'visibility_off'"
                :type="show1 ? 'text':'password'"
                @click:append="show1 = !show1"                   
                required>
                </v-text-field>
            </v-col>  
                                
            <v-col cols="12" md="12">
              <v-text-field 
                label="Nueva contraseña" 
                v-model="newPassword"
                :append-icon="show2 ? 'visibility' : 'visibility_off'"
                :type="show2 ? 'text':'password'"
                @click:append="show2 = !show2"                   
                required>
                </v-text-field>
            </v-col> 
                        
            <v-col cols="12" md="12">
              <v-text-field 
                label="Confirmar nueva contraseña" 
                v-model="confirmPassword"
                :append-icon="show3 ? 'visibility' : 'visibility_off'"
                :type="show3 ? 'text':'password'"
                @click:append="show3 = !show3"                   
                required>
                </v-text-field>
            </v-col> 
            
          </v-row>
        </v-container>      
      </v-form>
    </v-card-text>
    <v-card-actions>
      <div class="flex-grow-1"></div>
      <v-btn color="blue darken-1" text @click="changePassword()">Actualizar Contraseña</v-btn>
    </v-card-actions>
  </v-card>
    </v-layout>
  </v-container>
 
</template>

<script>
  import axios from 'axios'
  export default {
    data() {
      return {
        oldPassword:'',
        newPassword:'',
        confirmPassword:'',
        show1: false,
        show2:false,
        show3:false
      }
    },
    methods: {
      changePassword() {       
        if (this.newPassword === this.confirmPassword) {
          let me=this;                    
          let AuthorizationHeader = {"Authorization" : "Bearer " + this.$store.state.token}
          let headers = {headers:AuthorizationHeader}              
          axios.put('api/user/updatePassword/'+this.$store.state.user.IdUser,
          {
            'OldPassword':me.oldPassword,
            'NewPassword':me.newPassword            
          },headers)
          .then(function (response) {
            // handle success
            console.log('Update Successful')       
            clearFormChangePassword()                                                
          })
          .catch(function (error) {
            // handle error          
            console.log(error);  
            if (error.response.status === 401) {						
              me.$store.dispatch('exit')
            }       
          })
        }else{
          alert("las contraseñas no coinciden")
        }
      },
      clearFormChangePassword(){
         this.oldPassword = ''
         this.newPassword = ''
         this.confirmPassword = ''
      }        
    },
  }
</script>

